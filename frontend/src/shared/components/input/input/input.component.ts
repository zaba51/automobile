import { Component, Input, OnInit } from '@angular/core';
import { AbstractControl, FormControl } from '@angular/forms';

@Component({
  selector: 'app-input',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.scss']
})
export class InputComponent implements OnInit {
  @Input() title: string = '';
  @Input() placeholder: string = '';
  @Input() type: string = 'text';
  @Input() value: any = '';
  @Input() control: any = new FormControl(this.value);
  @Input() isDropdownOpen = false;
  @Input() dropdownItems: any[] = [];

  constructor() { }

  ngOnInit(): void {
  }


  onDropdownSelect(item: any) {
    this.control.patchValue(item.value);
    this.value = item.value;
  }

  trackBy(index: number, item: any) {
    return 1;
  }
}
