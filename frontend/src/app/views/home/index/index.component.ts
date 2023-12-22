import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent {

  constructor(private router: Router) { }

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

  onSearch(searchObj: {location: string, date: string, time: number}) {
    const queryParams = {
      date: searchObj.date,
      time: searchObj.time,
      location: searchObj.location,
    };


    this.router.navigate(['./search'], { queryParams });
  }
}