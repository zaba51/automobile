import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  @Output() close = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  goBack() {
    this.close.emit();
  }
}
