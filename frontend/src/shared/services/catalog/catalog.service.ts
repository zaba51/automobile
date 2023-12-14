import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { CatalogItem } from 'src/shared/types/catalogTypes';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  constructor(private http: HttpClient) { }

  getAvailableItems(searchRequest: any): Observable<CatalogItem[]> {
    return of(catalogItems);
  }

  getItemsByCompanyId(id: number) {
    return of(catalogItems);
  }

  getItemById(id: number) {
    return of(catalogItems.find(el => el.id === id))
  }
}


const catalogItems: CatalogItem[] = [
  {
      id: 1,
      model: {
          id: 101,
          name: 'Sedan X',
          power: 200,
          gear: 'Automatic',
          doorCount: 4,
          seatCount: 5,
          engine: 'V6',
          imageUrl: 'https://example.com/sedan-x-image.jpg',
          color: 'red'
      },
      price: 35000,
      company: 'Toyota'
  },
  {
      id: 2,
      model: {
          id: 102,
          name: 'SUV Y',
          power: 250,
          gear: 'Manual',
          doorCount: 5,
          seatCount: 7,
          engine: 'V8',
          imageUrl: 'https://example.com/suv-y-image.jpg',
          color: 'green'
      },
      price: 45000,
      company: 'Seat'
  },
  {
    id: 3,
    model: {
        id: 102,
        name: 'SUV Z',
        power: 250,
        gear: 'Manual',
        doorCount: 5,
        seatCount: 7,
        engine: 'V8',
        imageUrl: 'https://example.com/suv-y-image.jpg',
        color: 'blue'
    },
    price: 55000,
    company: 'Toyota'
},
];
