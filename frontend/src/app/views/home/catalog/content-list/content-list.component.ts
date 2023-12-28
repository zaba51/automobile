import { Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { CatalogItem, Model } from 'src/shared/types/catalogTypes';
import { FilterItem } from '../filter-panel/filter-panel.component';

@Component({
  selector: 'app-content-list',
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.scss']
})
export class ContentListComponent implements OnInit {
  @ViewChild('sidePanel') sidePanel: ElementRef;
  
  @Input() availableItems: CatalogItem[];

  @Output() rentClick = new EventEmitter();


  displayedItems: CatalogItem[];

  filterPanelOpen = false;
  sortPanelOpen = false;

  selectedOption: string = 'price-up';
  options = [
    { value: 'price-up', label: 'Price (up)' },
    { value: 'price-down', label: 'Price (down)' },
  ];
  
  get selectedSorting() {
    return this.options.find(x => x.value == this.selectedOption)
  }

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.displayedItems = this.availableItems; 
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const sidePanelElement = document.querySelector('.side-panel');
    const isClickInside = sidePanelElement && sidePanelElement.contains(event.target as Node);

    if (!isClickInside && (this.filterPanelOpen || this.sortPanelOpen)) {
      this.onPanelToggle(false);
      this.filterPanelOpen = false;
      this.sortPanelOpen = false;
    }
  }

  onToggleFilter(isOpen: boolean) {
    this.onPanelToggle(isOpen);
    this.filterPanelOpen = isOpen;
    this.sortPanelOpen = false;
  }

  onToggleSort(isOpen: boolean) {
    this.onPanelToggle(isOpen);
    this.sortPanelOpen = isOpen;
    this.filterPanelOpen = false;
  }

  onPanelToggle(isOpen: boolean) {
    if (isOpen) {
      document.body.classList.add('disable-scroll')
      document.body.classList.add('reset-overflow')
      this.sidePanel.nativeElement.style.top = `${window.scrollY}px`;
      // this.sidePanel.nativeElement.style.height = `${window.screen.height}px`;
      this.sidePanel.nativeElement.style.right = `0`;
      this.sidePanel.nativeElement.classList.add('slide-in')
    }
    else {
      document.body.classList.remove('disable-scroll')
      document.body.classList.remove('reset-overflow')
      this.sidePanel.nativeElement.classList.remove('slide-in')
    }
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
