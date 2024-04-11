import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../../api';
import { CatalogItem } from 'src/shared/types/catalogTypes';

export interface IReservationTransaction {
  guid: string,
  transactionTime: string,
  catalogItem: CatalogItem,
  userId: number,
  beginTime: string,
  endTime: string
}

@Injectable({
  providedIn: 'root'
})
export class SupplierService {
  constructor(private http: HttpClient) {}

  getMessagesForSupplier(supplierId: number): Observable<string[]> {
    return this.http.get<string[]>(API_URL + "/supplier/" + supplierId + '/messages');
  }
}