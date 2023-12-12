import { Component, OnInit, Output, EventEmitter } from '@angular/core';

export interface FilterSection {
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
  styleUrls: ['./filter-panel.component.css']
})
export class FilterPanelComponent implements OnInit {
  @Output() filterChange = new EventEmitter();

  private filterList: { [key: string]: FilterItem[] }

  constructor() { }

  ngOnInit(): void {
  }

  
  filterSections: FilterSection[] = [
    {
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
      headline: 'Price',
      items: [
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
          value: '300+'
        },
      ]
    },
    {
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
      headline: 'Color',
      items: [
        {
          label: 'Red',
          value: 'Red'
        },
        {
          label: 'Yellow',
          value: 'Yellow'
        },
        {
          label: 'Blue',
          value: 'Blue'
        },
        {
          label: 'Green',
          value: 'Green'
        },
        {
          label: 'Black',
          value: 'Black'
        },
        {
          label: 'Silver',
          value: 'Silver'
        },
        {
          label: 'White',
          value: 'White'
        },
      ]
    },
  ]


  onAddFilter(sectionName: string, item: FilterItem) {
    this.filterList[sectionName] = [
      ...this.filterList[sectionName],
      item
    ]

    this.filterChange.emit(this.filterList);
  }
}
