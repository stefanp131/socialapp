import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { AppUser } from 'src/app/_models/AppUser';
import { AccountService } from 'src/app/_services/account.service';
import { MessageBoardService } from 'src/app/_services/message-board.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class UserCardComponent implements OnInit {
  @Input('user') user: AppUser;

  like = false;
  likesCount = 0;
  constructor(
    private messageBoardService: MessageBoardService,
    private userService: UserService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.likesCount = this.user.likedByUsers.length;

    if (this.user.likedByUsers.length !== 0) {
      this.like = this.user.likedByUsers.some(
        (user) => user.sourceUserId == this.accountService.currentUserSource.value.id
      );
    }
  }

  toggleLike() {
    this.like = !this.like;

    if (this.like) {
      this.userService
        .createLikeForUser(this.user.id)
        .subscribe(() =>
          this.messageBoardService.setToggleLikeAction(this.user.id)
        );
    } else {
      this.userService
        .deleteLikeForUser(this.user.id)
        .subscribe(() =>
          this.messageBoardService.setToggleLikeAction(this.user.id)
        );
    }
  }
}
