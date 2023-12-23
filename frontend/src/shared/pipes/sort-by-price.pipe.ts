import { Pipe, PipeTransform } from '@angular/core';
import { CatalogItem } from '../types/catalogTypes';

@Pipe({
  name: 'sortByPrice'
})
export class SortByPricePipe implements PipeTransform {

  transform(items: CatalogItem[], type: string): CatalogItem[] {
    const copy = items.slice();

    if (type == 'price-up') return copy.sort((x,y) => x.price - y.price);
    if (type == 'price-down') return copy.sort((x,y) => y.price - x.price);
    else return copy;
  }

}
