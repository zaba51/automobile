import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CatalogItem } from 'src/shared/types/catalogTypes';
import { FilterItem } from '../filter-panel/filter-panel.component';

@Component({
  selector: 'app-content-list',
  templateUrl: './content-list.component.html',
  styleUrls: ['./content-list.component.css']
})
export class ContentListComponent implements OnInit {
  @Input() availableItems: CatalogItem[];

  displayedItems: CatalogItem[];

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  onActionClick(action: any) {
    if (action.type == 'rent') {
      this.router.navigate(['/details', action.id])
    }
  }

  public filterResults(filters: { [key: string]: FilterItem[] }) {
    // this.displayedItems = this.availableItems.filter(item => {
    // })

    // let visibleItems = this.availableItems;

    // Object.keys(filters).forEach(filter => {
    //   visibleItems = this.filterBy(filter, visibleItems);
    // });

  }

  // private filterBy(filter, visibleItems: CatalogItem[]) {
  //   switch (filter) {
  //     case 'Gear':
  //       break;
  //   }
  // }
}
