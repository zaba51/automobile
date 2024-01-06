import { Component, OnInit, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import { CatalogItem, Model } from 'src/shared/types/catalogTypes';

export interface FilterSection {
  group: string,
  headline: string,
  items: FilterItem[]
}

export interface FilterItem {
  label: string,
  value: string,
}

@Component({
  selector: 'app-filter-panel',
  templateUrl: './filter-panel.component.html',
  styleUrls: ['./filter-panel.component.scss']
})
export class FilterPanelComponent implements OnInit {
  @ViewChild('filterCheckboxes') filterCheckboxes: ElementRef;

  @Output() filterChange = new EventEmitter();

  private filterList: { [key: string]: string[] } = {}

  constructor() { }

  ngOnInit(): void {
  }

  
  filterSections: FilterSection[] = [
    {
      group: 'company',
      headline: 'Company',
      items: [
        {
          label: 'Toyota',
          value: 'Toyota',
        },
        {
          label: 'Volksvagen',
          value: 'Volksvagen',
        },
        {
          label: 'Ford',
          value: 'Ford',
        },
        {
          label: 'Ford',
          value: 'Ford',
        },
        {
          label: 'Ford',
          value: 'Ford',
        },
        {
          label: 'Hyundai',
          value: 'Hyundai',
        },
        {
          label: 'Honda',
          value: 'Honda',
        },
        {
          label: 'Seat',
          value: 'Seat',
        }
      ]
    },
    {
      group: 'price',
      headline: 'Price',
      items: [
        {
          label: '0-50',
          value: '0-50'
        },
        {
          label: '50-100',
          value: '50-100'
        },
        {
          label: '101-300',
          value: '101-300'
        },
        {
          label: '300+',
          value: '300-9999999'
        },
      ]
    },
    {
      group: 'gear',
      headline: 'Gear',
      items: [
        {
          label: 'Automatic',
          value: 'Automatic'
        },
        {
          label: 'Manual',
          value: 'Manual'
        }
      ]
    },
    {
      group: 'color',
      headline: 'Color',
      items: [
        {
          label: 'Red',
          value: 'red'
        },
        {
          label: 'Yellow',
          value: 'yellow'
        },
        {
          label: 'Blue',
          value: 'blue'
        },
        {
          label: 'Green',
          value: 'green'
        },
        {
          label: 'Black',
          value: 'black'
        },
        {
          label: 'Silver',
          value: 'silver'
        },
        {
          label: 'White',
          value: 'white'
        },
      ]
    },
  ]

  onClearFilters() {
    if (this.filterCheckboxes) {
      this.filterList = {};
      this.filterChange.emit(this.filterList);

      const checkboxes = this.filterCheckboxes.nativeElement.querySelectorAll('.checkbox') as HTMLInputElement[];

      if (checkboxes) {
        checkboxes.forEach((x) => { x.checked = false })
      }
    }
  }

  onAddFilter(group: string, item: FilterItem, checked: boolean) {
    if (checked) {
      const filterList = this.filterList[group] ?? []
  
      this.filterList[group] = [
        ...filterList,
        item.value.toLowerCase()
      ]
    }
    else {
      const filterList = this.filterList[group] ?? []
  
      this.filterList[group] = filterList.filter(f => f !== item.value.toLowerCase());
    }

    this.filterChange.emit(this.filterList);
  }
}
