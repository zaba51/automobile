import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { CatalogItem, Model } from 'src/shared/types/catalogTypes';
import { FilterItem } from '../filter-panel/filter-panel.component';

@Component({
  selector: 'app-content-list',
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.scss']
})
export class ContentListComponent implements OnInit {
  @Input() availableItems: CatalogItem[];

  @Output() rentClick = new EventEmitter();

  displayedItems: CatalogItem[];

  selectedOption: string = 'price-up';
  options = [
    { value: 'price-up', label: 'Price (up)' },
    { value: 'price-down', label: 'Price (down)' },
  ];

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.displayedItems = this.availableItems; 
  }

  onSort() {
    console.log(this.selectedOption);
  }

  onActionClick(action: any) {
    if (action.type == 'rent') {
      this.router.navigate(['/details', action.id], {state: {a: 'asd'}})

      this.rentClick.emit(action.id);
    }
  }

  public filterResults(filters: { [key: string]: string[] }) {
    let visibleItems = this.availableItems;

    Object.keys(filters).forEach(filter => {
      if (filter === 'company') {
        visibleItems = this.filterByCompany(filters['company'], visibleItems);
      }
      else if (filter === 'price') {
        visibleItems = this.filterByPrice(filters['price'], visibleItems);
      }
      else {
        visibleItems = this.filterBy(filter as keyof Model, filters[filter], visibleItems);
      }
    });
    
    this.displayedItems = visibleItems;
  }

  private filterByCompany(values: string[], visibleItems: CatalogItem[]) {
    if (values.length < 1) return visibleItems;
    return visibleItems.filter(item => values.includes(item.company))
  }

  private filterByPrice(values: string[], visibleItems: CatalogItem[]) {
    if (values.length < 1) return visibleItems;

    return visibleItems;
  }

  private filterBy(filter: keyof Model, values: string[],  visibleItems: CatalogItem[]) {
    if (values.length < 1) return visibleItems;

    return visibleItems.filter(item => values.includes( item.model[filter].toString() ))
  }
}
