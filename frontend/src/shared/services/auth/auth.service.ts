import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { AddUser, AppUser } from 'src/shared/types/userTypes';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  user: AppUser | null;

  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);

  isLoggedIn$ = this._isLoggedIn$.asObservable();
  
  private readonly TOKEN_NAME = 'auth_token';

  get token() {
    return localStorage.getItem(this.TOKEN_NAME);
  }
  
  constructor(private userService: UserService, private router: Router) { 
    this.user = this.token ? this.getUser(this.token) : null;

    const validToken = this.user !== null && this.user!.exp > Date.now() / 1000;

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

  private getUser(token: string): AppUser | null {
    try {
      const tokenUser = JSON.parse(atob(token.split('.')[1]));

      let str: string;
      const mappedUser = {} as any;
      for (let prop in tokenUser) {
        
        // map claims like "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" to "role"
        if (prop.includes('/')) {
          str = prop.substring(prop.lastIndexOf('/') + 1, prop.length);
        }
        else {
          str = prop;
        }

        mappedUser[str] = tokenUser[prop];;
      }
      console.log(mappedUser)
      return mappedUser as AppUser;
    }
    catch (error) {
      console.log(error);
      return null;
    }
  }
}
