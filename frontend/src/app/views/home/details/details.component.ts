import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { of, switchMap } from 'rxjs';
import { CatalogService } from 'src/shared/services/catalog/catalog.service';
import { CatalogItem, Model } from 'src/shared/types/catalogTypes';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  item: CatalogItem | "Loading" = 'Loading';
  id: number | null;

  name = new FormControl('');
  surname = new FormControl('');
  country = new FormControl('');
  number = new FormControl('');

  constructor(
    private router: Router,
    private catalogService: CatalogService,
    private route: ActivatedRoute  
  ) { }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      switchMap(params => {
        this.id = parseInt(params.get('id') ?? '');
  
        if (this.id) {
          return this.catalogService.getItemById(this.id);
        } else {
          return of(null)
        }
      })
    ).subscribe(item => {
      if (item) {
        this.item = item;
      }
    });

  }

  goBack() {
    this.router.navigate(['/search']);
  }
}
