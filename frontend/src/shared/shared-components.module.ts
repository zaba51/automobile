import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputComponent } from './components/input/input/input.component';
import { ButtonComponent } from './components/button/button/button.component';
import { FooterComponent } from './components/footer/footer.component';
import { MatIconModule } from '@angular/material/icon';
import { VehicleCardComponent } from './components/vehicleCard/vehicle-card/vehicle-card.component';
import { GoBackComponent } from './components/go-back/go-back.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SortByPricePipe } from './pipes/sort-by-price.pipe';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { EmptyStateComponent } from './components/empty-state/empty-state.component';


@NgModule({
  declarations: [
    InputComponent,
    ButtonComponent,
    FooterComponent,
    VehicleCardComponent,
    GoBackComponent,
    NavMenuComponent,
    SortByPricePipe,
    SpinnerComponent,
    EmptyStateComponent
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatIconModule,
    RouterModule,
    ReactiveFormsModule
  ],
  exports: [
    InputComponent,
    ButtonComponent,
    FooterComponent,
    VehicleCardComponent,
    GoBackComponent,
    NavMenuComponent,
    SortByPricePipe,
    SpinnerComponent,
    EmptyStateComponent
  ]
})
export class SharedComponentsModule { }
