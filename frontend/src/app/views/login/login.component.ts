import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/shared/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  error: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
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
}
