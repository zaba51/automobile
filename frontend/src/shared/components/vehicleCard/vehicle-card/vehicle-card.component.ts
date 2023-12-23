import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CatalogItem } from 'src/shared/types/catalogTypes';

@Component({
  selector: 'app-vehicle-card',
  templateUrl: './vehicle-card.component.html',
  styleUrls: ['./vehicle-card.component.css']
})
export class VehicleCardComponent implements OnInit {
  @Input() item: CatalogItem;

  @Input() isDelete: boolean = false;

  @Input() isRent: boolean = true;

  @Output() actionClick = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

  onRentClick() {
    this.actionClick.emit({
      type: 'rent',
      id: this.item.id
    });
  }
}
