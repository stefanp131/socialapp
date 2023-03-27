import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { AppUser } from 'src/app/_models/AppUser';

@Component({
  selector: 'user-card',
  templateUrl: './user-card.component.html',
  styleUrls: ['./user-card.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class UserCardComponent implements OnInit {
  @Input('user') user: AppUser;
  constructor() {}

  ngOnInit(): void {}
}
