import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AuthService } from 'src/shared/services/auth/auth.service';
import { CatalogService } from 'src/shared/services/catalog/catalog.service';
import { CatalogItem, Supplier } from 'src/shared/types/catalogTypes';

@Component({
  selector: 'app-offer-list',
  templateUrl: './offer-list.component.html',
  styleUrls: ['./offer-list.component.scss']
})
export class OfferListComponent implements OnInit {
  isDialogOpen = false;
  catalogItems: CatalogItem[] | "Loading" = "Loading";
  supplier: Supplier;
  searchControl = new FormControl('');
  displayItems: CatalogItem[] = [];

  constructor(private catalogService: CatalogService, private authService: AuthService) { }

  ngOnInit(): void {
    if (this.authService.user?.supplierId) {
      this.catalogService.getSupplierInfo(this.authService.user?.supplierId)
        .subscribe(({catalogItems, supplier}) => {
          this.catalogItems = catalogItems;
          this.supplier = supplier;
          this.onSearch('');
        })
    }

    this.searchControl.valueChanges.subscribe(value => this.onSearch(value));
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
    if (!isOpen) location.reload()
  }

  onSearch(value: string | null) {
    if (this.catalogItems == "Loading") return;

    if (!value) {
      this.displayItems = this.catalogItems;
    }
    else {
      this.displayItems = this.catalogItems.filter(item => {
        return item.model.name.toLowerCase().includes(value)
            || item.model.company.toLowerCase().includes(value)  
      })
    }
  }
}
