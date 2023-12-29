import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { CatalogService } from 'src/shared/services/catalog/catalog.service';
import { CatalogItem } from 'src/shared/types/catalogTypes';

export interface ISearchDetails {
  beginTime: string,
  location: string,
  duration: number,
}

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  availableItems: CatalogItem[] | 'Loading' = 'Loading';
  searchDetails: ISearchDetails;

  constructor(
    private route: ActivatedRoute,
    private catalogService: CatalogService,
    private router: Router
  ) {}

  ngOnInit() {
    // Subscribe to the queryParams observable
    this.route.queryParams.pipe(
      switchMap((params) => {
        this.searchDetails = {
          beginTime: new Date(params['date']).toISOString(),
          duration: params['time'],
          location: params['location']
        };
        return this.catalogService.getAvailableItems(this.searchDetails);
      })
    ).subscribe(items => {
      this.availableItems = items;
    });
  }

  onRentClick(itemId: number) {
    this.router.navigate(['/details', itemId], {state: { searchDetails: this.searchDetails }})
  }
}
