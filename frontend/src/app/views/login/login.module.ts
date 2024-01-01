import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedComponentsModule } from 'src/shared/shared-components.module';
import { RegisterFormComponent } from './register-form/register-form.component';
import { LoginFormComponent } from './login-form/login-form.component';

const routes: Routes = [
  {
    path: '**',
    component: LoginComponent
  }
];

@NgModule({
  declarations: [
    LoginComponent,
    RegisterFormComponent,
    LoginFormComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedComponentsModule
  ]
})
export class LoginModule { }
