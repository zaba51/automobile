import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  iconItems = [
    {
      icon: "A",
      text: "Choose location"
    },
    {
      icon: "B",
      text: "Pick-up date"
    },
    {
      icon: "C",
      text: "Choose your car"
    }
  ]
}
