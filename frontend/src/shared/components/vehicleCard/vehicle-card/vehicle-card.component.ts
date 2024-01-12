import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { BASE_URL } from 'src/shared/api';
import { CatalogItem } from 'src/shared/types/catalogTypes';

@Component({
  selector: 'app-vehicle-card',
  templateUrl: './vehicle-card.component.html',
  styleUrls: ['./vehicle-card.component.scss']
})
export class VehicleCardComponent implements OnInit {
  @Input() item: CatalogItem;

  @Input() isDelete: boolean = false;

  @Input() isRent: boolean = true;

  @Input() isPrice: boolean = true;

  @Input() isLogo: boolean = true;

  @Output() actionClick = new EventEmitter();

  BASE_URL = BASE_URL;

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
