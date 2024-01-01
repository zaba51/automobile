import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { markFormGroupTouched } from 'src/shared/helpers/validation';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss']
})
export class RegisterFormComponent {
  @Output() register = new EventEmitter<FormGroup>();

  form = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    confirmPassword: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });


  onRegisterClick() {
    markFormGroupTouched(this.form);

    if (this.form.valid) {
      this.register.emit(this.form);
    }
  }
}
