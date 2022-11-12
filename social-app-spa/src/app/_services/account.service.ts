import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  BehaviorSubject,
  map,
} from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';


@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  public currentUserSource = new BehaviorSubject<User>(null);

  constructor(private http: HttpClient) {}

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    const token = this.getDecodedToken(user.token);
    user.roles = [];
    const roles = token.role;
    Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
    user.id = token.nameid;
    user.username = token.unique_name;
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }

  getDecodedToken(token) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  updateProfilePicture(id: number, profilePicture: string) {
    return this.http.patch(`${this.baseUrl}account/${id.toString()}`, {
      profilePicture: profilePicture,
    });
  }

  getProfilePicture(id: number) {
    return this.http.get(
      this.baseUrl + 'account/' + this.currentUserSource.value.id + '/picture'
    );
  }
}
