import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
import { AuthService } from 'src/shared/services/auth/auth.service';
import { AddUser } from 'src/shared/types/userTypes';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  error: string = '';
  activeForm: string = 'login';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.activeForm = this.router.routerState.snapshot.url.split('auth/')[1];

    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.activeForm = event.url.split('auth/')[1];
      }
    });
  }

  login(form: FormGroup) {
    this.authService.login(form.get('email')?.value, form.get('password')?.value)
      .subscribe({
        next: (r) => this.router.navigate(['/']),
        error: (e) => {
          if (e.status == 401) {
            this.error = "Invalid email or password"
          }
        }
      })
  }

  register(form: FormGroup) {
    const newUser: AddUser = {
      email: form.get('email')?.value,
      password: form.get('password')?.value
    }

    this.authService.register(newUser)
      .subscribe({
        next: (r) => this.router.navigate(['/']),
        error: (e) => {
          if (e.status == 409) {
            this.error = "Email is taken."
          }
        }
      })
  }
}
