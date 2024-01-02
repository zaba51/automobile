import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CatalogService } from 'src/shared/services/catalog/catalog.service';
import { Location } from 'src/shared/types/catalogTypes';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss']
})
export class IndexComponent {
  locations: Location[] = [];

  constructor(private router: Router, private catalogService: CatalogService) { }

  iconItems = [
    {
      icon: "location_on",
      text: "Choose location"
    },
    {
      icon: "date_range",
      text: "Pick-up date"
    },
    {
      icon: "view_list",
      text: "Choose your car"
    }
  ]

  ngOnInit() {
    this.catalogService.getLocations().subscribe((list: Location[]) => this.locations = list);
  }

  onSearch(searchObj: {locationId: number, date: string, time: number}) {
    const queryParams = {
      date: searchObj.date,
      time: searchObj.time,
      locationId: searchObj.locationId,
    };


    this.router.navigate(['./search'], { queryParams });
  }
}