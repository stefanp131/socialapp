import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/AppUser';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ) {}

  getUsers(): Observable<AppUser[]> {
    return this.http.get<AppUser[]>(this.baseUrl + 'user');
  }

  getUser(id: number): Observable<AppUser> {
    return this.http.get<AppUser>(this.baseUrl + 'user/' + id);
  }

  getUsersByStringTerm(stringTerm: string): Observable<AppUser[]> {
    let params = new HttpParams();
    if (stringTerm) {
      params = params.append('stringTerm', stringTerm);
    }

    return this.http.get<AppUser[]>(this.baseUrl + 'user', { params: params });
  }

  createLikeForUser(targetUserId: number) {
    return this.http.post(this.baseUrl + 'user', {
      sourceUserId: this.accountService.currentUserSource.value.id,
      targetUserId: targetUserId,
    });
  }

  deleteLikeForUser(targetUserId: number) {
    let params = new HttpParams();
    params = params.append('targetId', targetUserId);
    params = params.append(
      'sourceId',
      this.accountService.currentUserSource.value.id
    );

    return this.http.delete(this.baseUrl + 'user', { params: params });
  }
}
