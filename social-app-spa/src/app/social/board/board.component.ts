import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import {
  debounceTime,
  map,
  merge,
  Observable,
  Subscription,
  switchMap,
  tap,
} from 'rxjs';
import { AppUser } from 'src/app/_models/AppUser';
import { BoardService } from 'src/app/_services/board.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss'],
})
export class BoardComponent implements OnInit {
  cachedUsers: AppUser[];
  appUsersSearch$: Observable<AppUser[]>;
  appUsersSearchWithLike$: Observable<AppUser[]>;
  searchForm: FormGroup;

  constructor(
    public userService: UserService,
    private boardService: BoardService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.searchForm = this.formBuilder.group({
      searchInput: [''],
    });

    this.appUsersSearch$ = merge(
      this.userService
        .getUsers()

        .pipe(
          map((users) => this.mapUsers(users)),
          tap((users) => (this.cachedUsers = users))
        ),
      this.searchForm.get('searchInput').valueChanges.pipe(
        debounceTime(400),
        switchMap((searchValue) =>
          this.userService.getUsersByStringTerm(searchValue).pipe(
            map((users) => this.mapUsers(users)),
            tap((users) => (this.cachedUsers = users))
          )
        )
      )
    );

    const toggleLikeAction = this.boardService.getToggleLikeAction().pipe(
      switchMap((id) =>
        this.userService.getUser(id).pipe(
          map((user) => {
            const listUser = this.cachedUsers.find((x) => x.id === user.id);
            console.log(listUser);
            const replaceUser: AppUser = {
              ...user,
              profilePicture:
                user?.profilePicture ?? '../../../assets/empty-profile-pic.png',
            };

            this.cachedUsers[this.cachedUsers.indexOf(listUser)] = replaceUser;

            return this.cachedUsers;
          })
        )
      )
    );

    this.appUsersSearchWithLike$ = merge(
      this.appUsersSearch$,
      toggleLikeAction
    );
  }

  private mapUsers(users: AppUser[]): AppUser[] {
    return users.map((user) => ({
      ...user,
      profilePicture:
        user?.profilePicture ?? '../../../assets/empty-profile-pic.png',
    }));
  }
}
