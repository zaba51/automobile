import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile.component';
import { SharedComponentsModule } from 'src/shared/shared-components.module';
import { RouterModule, Routes } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { OfferListComponent } from './offer-list/offer-list.component';
import { VehicleFormComponent } from './vehicle-form/vehicle-form.component';
import { ReservationsComponent } from './reservations/reservations.component';
import { isAuthenticatedGuard } from 'src/app/guards/is-authenticated.guard';
import { isSupplierGuard } from 'src/app/guards/is-supplier.guard';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

const routes: Routes = [
  { path: '',
    component: ProfileComponent,
    canActivate: [isAuthenticatedGuard],
    children: [
      {
        path: '',
        redirectTo: 'reservations',
        pathMatch: 'full'
      },
      {
        path: 'offer',
        component: OfferListComponent,
        canActivate: [isSupplierGuard],
      },
      {
        path: 'reservations',
        component: ReservationsComponent
      }
    ],
  }
];

@NgModule({
  declarations: [
    ProfileComponent,
    OfferListComponent,
    VehicleFormComponent,
    ReservationsComponent
  ],
  imports: [
    CommonModule,
    SharedComponentsModule,
    RouterModule.forChild(routes),
    MatIconModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class ProfileModule { }
