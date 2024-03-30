import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { SharedComponentsModule } from 'src/shared/shared-components.module';
import { CustomInterceptor } from './interceptors/custom.interceptor';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', loadChildren: () => import('./views/home/home.module').then(m => m.HomeModule) },
      { path: 'auth', loadChildren: () => import('./views/login/login.module').then(m => m.LoginModule) },
      { path: 'profile', loadChildren: () => import('./views/profile/profile.module').then(m => m.ProfileModule) },
    ]),
    SharedComponentsModule,
    BrowserAnimationsModule
  ],
  providers: [
    [
      {
        provide: HTTP_INTERCEPTORS,
        useClass: CustomInterceptor,
        multi: true
      },
      {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthInterceptor,
        multi: true
      }
    ]
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
