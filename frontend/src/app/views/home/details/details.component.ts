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
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  item: CatalogItem | "Loading" = 'Loading';
  id: number | null;
  searchDetails: ISearchDetails;
  beginTime: Date;
  endTime: Date;

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
    this.beginTime = new Date(this.searchDetails.beginTime);
    this.endTime = new Date(this.beginTime.getTime() + this.searchDetails.duration * 1000 * 60 * 60);
  }

  onRentClick() {
    if (this.item === 'Loading') return;

    markFormGroupTouched(this.form);

    if (this.form.valid) {
      const date = new Date(this.searchDetails.beginTime);

      const newItem: AddReservationDTO = {
        userId: 1,
        catalogItemId: this.item.id,
        beginTime: this.searchDetails.beginTime,
        endTime: this.endTime.toISOString(),
        driversDetails: {
          name: this.form.get('name')?.value as string,
          surname: this.form.get('surname')?.value as string,
          country: this.form.get('country')?.value as string,
          number: this.form.get('number')?.value as string,
        }
      };

      this.reservationsService.addReservation(1, newItem).subscribe(result => {
        if (result === true) {
          this.router.navigate(['/profile'])
        }
      });
    }
  }

  goBack() {
    // this.router.navigate(['/search']);
    this.location.back();
  }
}
