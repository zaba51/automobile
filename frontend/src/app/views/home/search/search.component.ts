import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Location } from 'src/shared/types/catalogTypes';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  @Input() locations: Location[] = [];
  @Output() search = new EventEmitter();

  location = new FormControl<string>('');
  date = new FormControl();
  time = new FormControl();
  minDate: string;

  isDropdownOpen = false;

  constructor() { }

  get dropdownItems() {
    return this.locations.map(location => {
      return {
        ...location,
        label: `${location.cityName}, ${location.countryName}`,
        value: location.cityName
      }  
    })
  }

  get locationNames(): string[] {
    return this.locations.map(x => x.cityName.toLowerCase());
  }

  ngOnInit(): void {
    this.location.setValue('Cracow');
    this.minDate = new Date().toISOString().split('.')[0].slice(0, -3)
    this.date.setValue(this.minDate);
    this.time.setValue(10);

    this.location.valueChanges.subscribe(value => {
      if (value === null ||
          value.length <= 0 ||
          this.locationNames.includes(value.toLowerCase())) 
      {
        this.isDropdownOpen = false;
      }
      else {
        this.isDropdownOpen = true;
      }
    })
  }

  onClick() {
    console.log(this.location, this.date, this.time);
    this.search.emit({
      locationId: this.locations.find(x => x.cityName === this.location.value)?.id || 1,
      date: this.date.value + ':00.000Z',
      time: this.time.value
    });
  }
}
