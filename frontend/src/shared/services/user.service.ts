import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_URL } from '../api';
import { Observable, of, throwError } from 'rxjs';
import { AddUser, AppUser, User } from '../types/userTypes';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  getById(userId: number) {
    return this.http.get<User>(API_URL + "/users/" + userId);
  }

  login(email: string, password: string): Observable<string> {
    return this.http.post(
      API_URL + '/authentication/authenticate',
      { email, password },
      { responseType: 'text' }
    );
  }

  logout() {
    return this.http.get(API_URL + '/authentication/logout')
  }

  register(newUser: AddUser): Observable<string> {
    return this.http.post(API_URL + '/users', newUser, { responseType: 'text' });
  }

  isSessionValid() {
    // return this.http.get<boolean>(API_URL + '/authentication/ping');
    return of(true);
  }
}


const users: User[] = [
  {
    id: 1,
    name: 'Adrian',
    email: 'adrian@gmail.com',
  }
]