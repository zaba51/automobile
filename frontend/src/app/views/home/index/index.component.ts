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

  onSearch() {
    const queryParams = {
      date: '2023-01-01',
      time: '12:00',
      location: 'City Center',
    };


    this.router.navigate(['./search'], { queryParams });
  }
}