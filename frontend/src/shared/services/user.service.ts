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
    // return this.http.post(
    //   API_URL + '/authentication/authenticate',
    //   { email, password },
    //   { responseType: 'text' }
    // );
    const user = users.find(user => user.email === email);
    if (!user) {
      return throwError(() => new HttpErrorResponse({status: 401}))
    }
    const appUser: AppUser = {
      sub: user.id,
      email: user.email,
      role: 'regular',
      exp: Date.now() + 1000 * 60 * 10,
    }
    return of('token.' + btoa(JSON.stringify(appUser)));
  }

  register(newUser: AddUser): Observable<User> {
    return this.http.post<User>(API_URL + '/users/', newUser);
  }
}


const users: User[] = [
  {
    id: 1,
    name: 'Adrian',
    email: 'adrian@gmail.com',
  }
]