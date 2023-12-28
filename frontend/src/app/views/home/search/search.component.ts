import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  @Output() search = new EventEmitter();

  location = new FormControl();
  date = new FormControl();
  time = new FormControl();

  constructor() { }

  ngOnInit(): void {
    this.location.setValue('Cracow');
    this.date.setValue(new Date().toLocaleString().split(',')[0]);
    this.time.setValue(10);
  }

  onClick() {
    console.log(this.location, this.date, this.time);
    this.search.emit({
      location: this.location.value,
      date: this.date.value,
      time: this.time.value
    });
  }
}
