import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { markFormGroupTouched } from 'src/shared/helpers/validation';
import { CatalogService } from 'src/shared/services/catalog/catalog.service';
import { AddItemDTO, CatalogItem, Location, Supplier } from 'src/shared/types/catalogTypes';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.scss']
})
export class VehicleFormComponent implements OnInit {
  @Input() locations: Location[] = [];
  @Input() supplier: Supplier;
  @Output() close = new EventEmitter();

  form = new FormGroup({
    name: new FormControl('', Validators.required),
    company: new FormControl('', Validators.required),
    power: new FormControl(0, Validators.required),
    gear: new FormControl('', Validators.required),
    doorCount: new FormControl(0, Validators.required),
    seatCount: new FormControl(0, Validators.required),
    engine: new FormControl('', Validators.required),
    location: new FormControl('', Validators.required),
    price: new FormControl<number>(0, Validators.required),
    color: new FormControl<string>('', Validators.required)
  });

  isDropdownOpen = false;

  file: File;

  src: string = '';

  get dropdownItems() {
    return this.locations.map(location => {
      return {
        ...location,
        label: `${location.cityName}, ${location.countryName}`,
        value: location.cityName
      }  
    })
  }
  
  constructor(private catalogService: CatalogService) { }

  get locationNames(): string[] {
    return this.locations.map(x => x.cityName.toLowerCase());
  }

  ngOnInit(): void {
    this.form.get('location')?.valueChanges.subscribe(value => {
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

  onSave() {
    markFormGroupTouched(this.form);

    if (this.form.valid) {
      const newItem: AddItemDTO = {
        model: {
          power: this.form.value.power as number,
          gear: this.form.value.gear as string,
          engine: this.form.value.engine as string,
          name: this.form.value.name as string,
          company: this.form.value.company as string,
          doorCount: this.form.value.doorCount as number,
          seatCount: this.form.value.seatCount as number,
          color: this.form.value.color as  string,
          imageUrl: '',
        },
        price: this.form.value.price as number,
        supplierId: this.supplier.id,
        locationId: this.locations.find(x => x.cityName === this.form.value.location)?.id || 1,
      };

      const formData: FormData = new FormData();
      formData.append('newItem', JSON.stringify(newItem));
      if (this.file) {
        formData.append('file', this.file, this.file.name);
      }
      
      this.catalogService.addItem(formData).subscribe();

      // this.close.emit();
    }
  }

  onFileSelected(event: any) {
    this.file = event.target.files[0];

    this.src = URL.createObjectURL(this.file);
  }

  goBack() {
    this.close.emit();
  }
}
