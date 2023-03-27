import { AfterViewInit, Component, OnInit, ViewEncapsulation } from '@angular/core';
import { toHTML } from 'ngx-editor';
import { map, Observable } from 'rxjs';
import { AppUser } from 'src/app/_models/AppUser';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-message-board',
  templateUrl: './message-board.component.html',
  styleUrls: ['./message-board.component.scss'],
})
export class MessageBoardComponent implements OnInit, AfterViewInit {
  appUsers$: Observable<AppUser[]>;
  constructor(public userService: UserService) {}

  classInitialised = false;

  ngOnInit(): void {
    this.appUsers$ = this.userService
      .getUsers()
      .pipe(
        map((users) =>
          users.map((user) => ({
            ...user,
            profilePicture: user?.profilePicture ?? '../../../assets/empty-profile-pic.png',
            description: user.description ?  toHTML(
              JSON.parse(
                user.description
              )
            ) : 'No description yet',
          }))
        )
      );
  }

  ngAfterViewInit(): void {
    this.classInitialised = true;
  }
}
