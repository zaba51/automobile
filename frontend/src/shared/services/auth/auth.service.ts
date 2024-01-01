import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { AddUser, AppUser } from 'src/shared/types/userTypes';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this._isLoggedIn$.asObservable();
  private readonly TOKEN_NAME = 'auth_token';
  user: AppUser | null;

  get token() {
    return localStorage.getItem(this.TOKEN_NAME);
  }

  constructor(private userService: UserService, private router: Router) { 
    this.user = this.token ? this.getUser(this.token) : null;

    const validToken = this.user !== null && this.user!.exp > Date.now();

    this._isLoggedIn$.next(!!this.token && validToken);
  }
  
  login(email: string, password: string) {
    return this.userService.login(email, password).pipe(
      tap(resToken => {
        this.setUser(resToken);
      })
    )
  }

  logout() {
    this.userService.logout().subscribe(() => {
      localStorage.removeItem(this.TOKEN_NAME);
      this._isLoggedIn$.next(false);
      this.user = null;
      this.router.navigate(['/']);
    })
  }

  register(newUser: AddUser) {
    return this.userService.register(newUser).pipe(
      tap(resToken => {
        this.setUser(resToken);
      })
    )
  }

  private setUser(token: string) {
    this._isLoggedIn$.next(true);
    localStorage.setItem(this.TOKEN_NAME, token);
    this.user = this.getUser(token);
  }

  private getUser(token: string): AppUser {
    return JSON.parse(atob(token)) as AppUser;
  }

  refreshToken() {
    const validToken = this.user !== null && this.user!.exp > Date.now();

    if (!validToken) {
      localStorage.removeItem(this.TOKEN_NAME);
      this._isLoggedIn$.next(false);
      this.user = null;
    }

    else {
      localStorage.removeItem(this.TOKEN_NAME);
      this.user!.exp = Date.now() + 1000 * 60;
      localStorage.setItem(this.TOKEN_NAME, btoa(JSON.stringify(this.user)));
    }
  }
}
