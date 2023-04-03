import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { toHTML } from 'ngx-editor';
import { concatMap, map, Observable, of, Subscription, switchMap } from 'rxjs';
import { AppUser } from 'src/app/_models/AppUser';
import { AppUserLike } from 'src/app/_models/AppUserLike';
import { BoardService } from 'src/app/_services/board.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss'],
})
export class BoardComponent implements OnInit {
  appUsers$: Observable<AppUser[]>;
  messageBoardServiceSubscription: Subscription;
  constructor(
    public userService: UserService,
    private boardService: BoardService
  ) {}

  classInitialised = false;

  ngOnInit(): void {
    this.appUsers$ = this.userService
      .getUsers()
      .pipe(map((users) => this.mapUsers(users)));

    this.boardService
      .getToggleLikeAction()
      .pipe(
        switchMap(() =>
          this.userService.getUsers().pipe(map((users) => this.mapUsers(users)))
        )
      )
      .subscribe((users) => (this.appUsers$ = of(users)));
  }

  private mapUsers(users: AppUser[]): AppUser[] {
    return users.map((user) => ({
      ...user,
      profilePicture:
        user?.profilePicture ?? '../../../assets/empty-profile-pic.png',
      description: user.description
        ? toHTML(JSON.parse(user.description))
        : 'No description yet',
    }));
  }

  identify(index: number, item: AppUser): number {
    return item.id;
  }
}
