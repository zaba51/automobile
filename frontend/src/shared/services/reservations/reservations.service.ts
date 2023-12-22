import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { CatalogItem } from 'src/shared/types/catalogTypes';

export interface IReservation {
  id: number,
  userId: number,
  catalogItem: CatalogItem,
  beginDate: string,
  endDate: string,
  driversDetails: IDriversDetails
}

export interface AddReservationDTO {
  userId: number,
  catalogItem: CatalogItem,
  beginDate: string,
  endDate: string,
  driversDetails: IDriversDetails
}

export interface IDriversDetails {
  name: string,
  surname: string,
  country: string,
  number: string,
}

@Injectable({
  providedIn: 'root'
})
export class ReservationsService {
  reservations = reservations;

  constructor(private http: HttpClient) { }

  getReservations(userId: number): Observable<IReservation[]> {
    if (!this.reservations[userId]) return of([]);
    else return of(this.reservations[userId])
  }

  addReservation(reservationRequest: AddReservationDTO): Observable<boolean> {
    const newReservation: IReservation = {
      id: Math.floor(Math.random() * 10000),
      userId: reservationRequest.userId,
      catalogItem: reservationRequest.catalogItem,
      beginDate: reservationRequest.beginDate,
      endDate: reservationRequest.endDate,
      driversDetails: reservationRequest.driversDetails
    }

    if (!this.reservations[newReservation.userId]) {
      this.reservations[newReservation.userId] = []
    }

    this.reservations[newReservation.userId].push(newReservation);

    return of(true);
  }

  deleteReservation(userId: number, reservationId: number): Observable<boolean> {
    if (this.reservations[userId]) {
      this.reservations[userId] = this.reservations[userId].filter(x => x.id !== reservationId);
      return of(true)
    }

    return of(false);
  }
}

const reservations: {[id: string]: IReservation[]} = {
  1: [{
    id: 123,
    userId: 1,
    catalogItem: {
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
    beginDate: '12.12.1121',
    endDate: '12.12.1121',
    driversDetails: {
      name: 'asda',
      surname: 'adsasdasd',
      country: 'adsasdasd',
      number: 'adsasdasd',
    }
  },
  {
    id: 1234,
    userId: 1,
    catalogItem: {
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
    beginDate: '12.12.1121',
    endDate: '12.12.1121',
    driversDetails: {
      name: 'asda',
      surname: 'adsasdasd',
      country: 'adsasdasd',
      number: 'adsasdasd',
    }
  }]
};

