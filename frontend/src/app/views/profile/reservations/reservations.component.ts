import { Component, OnInit } from '@angular/core';
import { IReservation, ReservationsService } from 'src/shared/services/reservations/reservations.service';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements OnInit {
  reservations: IReservation[];

  constructor(
    private reservationsService: ReservationsService
  ) { }

  ngOnInit(): void {
    this.reservationsService.getReservations(1).subscribe(reservations => this.reservations = reservations);
  }

  delete(reservationId: number) {
    this.reservationsService.deleteReservation(1, reservationId).subscribe(success => {
      if (success) {
        this.reservations = this.reservations.filter(x => x.id !== reservationId);
      }
    });
  }

  viewDetails(reservationId: number) {
    
  }
}
