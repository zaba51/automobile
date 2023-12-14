import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  @Output() close = new EventEmitter();
  company   = new FormControl('');
  gear      = new FormControl('');
  doorCount = new FormControl();
  seatCount = new FormControl();
  engine    = new FormControl('');
  available = new FormControl('');

  constructor() { }

  ngOnInit(): void {
  }

  goBack() {
    this.close.emit();
  }
}
