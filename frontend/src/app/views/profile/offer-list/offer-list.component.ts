import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CatalogService } from 'src/shared/services/catalog/catalog.service';
import { CatalogItem } from 'src/shared/types/catalogTypes';

@Component({
  selector: 'app-offer-list',
  templateUrl: './offer-list.component.html',
  styleUrls: ['./offer-list.component.scss']
})
export class OfferListComponent implements OnInit {
  isDialogOpen = false;

  catalogItems: CatalogItem[] | "Loading" = "Loading";

  constructor(private catalogService: CatalogService) { }

  ngOnInit(): void {
    this.catalogService.getItemsBySupplierId(1)
      .subscribe((catalogItems: CatalogItem[]) => {
        this.catalogItems = catalogItems;
      })
  }

  addNewVehicle() {
    this.toggleModal(true);
  }

  onActionClick(action: any) {
    if (action.type == 'rent') {
      this.toggleModal(true);
    }
  }

  toggleModal(isOpen: boolean) {
    this.isDialogOpen = isOpen;
  }
}
