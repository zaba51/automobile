import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { markFormGroupTouched } from 'src/shared/helpers/validation';
import { CatalogService } from 'src/shared/services/catalog/catalog.service';
import { AddItemDTO, CatalogItem } from 'src/shared/types/catalogTypes';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.scss']
})
export class VehicleFormComponent implements OnInit {
  @Output() close = new EventEmitter();

  form = new FormGroup({
    name: new FormControl('', Validators.required),
    company: new FormControl('', Validators.required),
    power: new FormControl(0, Validators.required),
    gear: new FormControl('', Validators.required),
    doorCount: new FormControl(0, Validators.required),
    seatCount: new FormControl(0, Validators.required),
    engine: new FormControl('', Validators.required),
    available: new FormControl('', Validators.required),
    price: new FormControl<number>(0, Validators.required),
    color: new FormControl<string>('', Validators.required)
  });

  constructor(private catalogService: CatalogService) { }

  ngOnInit(): void {
  }

  onSave() {
    markFormGroupTouched(this.form);

    if (this.form.valid) {
      const newItem: AddItemDTO = {
        model: {
          power: this.form.value.power as number,
          gear: this.form.value.gear as string,
          engine: this.form.value.engine as string,
          name: this.form.value.name as string,
          doorCount: this.form.value.doorCount as number,
          seatCount: this.form.value.seatCount as number,
          color: this.form.value.color as  string,
          imageUrl: '',
        },
        price: this.form.value.price as number,
        company: 'New Company'
      };

      this.catalogService.addItem(1, newItem);

      this.close.emit();
    }
  }

  goBack() {
    this.close.emit();
  }
}
