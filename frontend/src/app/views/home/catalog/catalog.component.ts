import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs';
import { CatalogService } from 'src/shared/services/catalog/catalog.service';
import { CatalogItem } from 'src/shared/types/catalogTypes';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent implements OnInit {
  availableItems: CatalogItem[];

  constructor(private route: ActivatedRoute, private catalogService: CatalogService) {}

  ngOnInit() {
    // Subscribe to the queryParams observable
    this.route.queryParams.pipe(
      switchMap((params) => {
        const getAvailableItemsRequest = {
          date: params['date'],
          time: params['time'],
          location: params['location']
        };
        return this.catalogService.getAvailableItems(getAvailableItemsRequest);
      })
    ).subscribe(items => {
      this.availableItems = items;
    });
  }
}
