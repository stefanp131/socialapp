import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AppUser } from 'src/app/_models/AppUser';
import { AccountService } from 'src/app/_services/account.service';
import { BoardService } from 'src/app/_services/board.service';
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
    private boardService: BoardService,
    private userService: UserService,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.likesCount = this.user.likedByUsers.length;

    if (this.user.likedByUsers.length !== 0) {
      this.like = this.user.likedByUsers.some(
        (user) =>
          user.sourceUserId == this.accountService.currentUserSource.value.id
      );
    }
  }

  toggleLike() {
    this.like = !this.like;

    if (this.like) {
      this.userService
        .createLikeForUser(this.user.id)
        .subscribe(() => this.boardService.setToggleLikeAction(this.user.id));
    } else {
      this.userService
        .deleteLikeForUser(this.user.id)
        .subscribe(() => this.boardService.setToggleLikeAction(this.user.id));
    }
  }

  goToProfile(viewedProfileId: number) {
    this.userService
      .createViewForUser(
        this.accountService.currentUserSource.value.id,
        viewedProfileId,
        this.accountService.currentUserSource.value.username
      )
      .subscribe(() => this.router.navigate(['./profile', viewedProfileId]));
  }
}
