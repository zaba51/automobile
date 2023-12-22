import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { markFormGroupTouched } from 'src/shared/helpers/validation';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {
  @Output() login = new EventEmitter<FormGroup>();

  form = new FormGroup({
    email: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  });


  onLoginClick() {
    markFormGroupTouched(this.form);

    if (this.form.valid) {
      // const : AddItemDTO = {
      //   model: {
      //     power: this.form.value.power as number,
      //     gear: this.form.value.gear as string,
      //     engine: this.form.value.engine as string,
      //     name: this.form.value.name as string,
      //     doorCount: this.form.value.doorCount as number,
      //     seatCount: this.form.value.seatCount as number,
      //     color: this.form.value.color as  string,
      //     imageUrl: '',
      //   },
        // price: this.form.value.price as number,
        // company: 'New Company'
      // };
      this.login.emit(this.form);
    }
  }
}
