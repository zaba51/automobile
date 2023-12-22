import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of, switchMap } from 'rxjs';
import { markFormGroupTouched } from 'src/shared/helpers/validation';
import { CatalogService } from 'src/shared/services/catalog/catalog.service';
import { AddReservationDTO, ReservationsService } from 'src/shared/services/reservations/reservations.service';
import { CatalogItem, Model } from 'src/shared/types/catalogTypes';
import { ISearchDetails } from '../catalog/catalog.component';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  item: CatalogItem | "Loading" = 'Loading';
  id: number | null;
  searchDetails: ISearchDetails;

  form = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    surname: new FormControl<string>('', Validators.required),
    country: new FormControl<string>('', Validators.required),
    number: new FormControl<string>('', Validators.required),
  });

  constructor(
    private router: Router,
    private catalogService: CatalogService,
    private route: ActivatedRoute,
    private location: Location,
    private reservationsService: ReservationsService
  ) { }


  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        this.id = parseInt(params.get('id') ?? '');
  
        if (this.id) {
          return this.catalogService.getItemById(this.id);
        } else {
          return of(null)
        }
      })
    ).subscribe(item => {
      if (item) {
        this.item = item;
      }
    });

    this.searchDetails = (this.location.getState() as any)?.searchDetails;
  }

  onRentClick() {
    if (this.item === 'Loading') return;

    markFormGroupTouched(this.form);

    if (this.form.valid) {
      const newItem: AddReservationDTO = {
        userId: 1,
        catalogItem: this.item,
        beginDate: this.searchDetails.date,
        endDate: this.searchDetails.date + this.searchDetails.time,
        driversDetails: {
          name: this.form.get('name')?.value as string,
          surname: this.form.get('surname')?.value as string,
          country: this.form.get('country')?.value as string,
          number: this.form.get('number')?.value as string,
        }
      };

      this.reservationsService.addReservation(newItem).subscribe(result => {
        if (result === true) {
          this.router.navigate(['/profile'])
        }
      });
    }
  }

  goBack() {
    this.router.navigate(['/search']);
  }
}
