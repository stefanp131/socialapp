import { AfterViewInit, Component, OnInit } from '@angular/core';
import { toHTML } from 'ngx-editor';
import { concatMap, map, Observable, Subscription, switchMap } from 'rxjs';
import { AppUser } from 'src/app/_models/AppUser';
import { MessageBoardService } from 'src/app/_services/message-board.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-message-board',
  templateUrl: './message-board.component.html',
  styleUrls: ['./message-board.component.scss'],
})
export class MessageBoardComponent implements OnInit, AfterViewInit {
  appUsers$: Observable<AppUser[]>;
  messageBoardServiceSubscription: Subscription;
  constructor(
    public userService: UserService,
    private messageBoardService: MessageBoardService
  ) {}

  classInitialised = false;

  ngOnInit(): void {
    this.appUsers$ = this.userService.getUsers().pipe(
      map((users) =>
        users.map((user) => ({
          ...user,
          profilePicture:
            user?.profilePicture ?? '../../../assets/empty-profile-pic.png',
          description: user.description
            ? toHTML(JSON.parse(user.description))
            : 'No description yet',
        }))
      )
    );

    const swapAppUsers$ = this.appUsers$;

    this.messageBoardService
      .getToggleLikeAction()
      .pipe(switchMap((id) => this.userService.getUser(id)))
      .subscribe((user: AppUser) => {
        console.log(user);
        this.appUsers$ = swapAppUsers$.pipe(
          map((users) => {
            const listUser = users.find((listUser) => listUser.id === user.id);
            listUser.likedByUsers = user.likedByUsers;

            return users;
          })
        );
      });
  }

  ngAfterViewInit(): void {
    this.classInitialised = true;
  }
}
