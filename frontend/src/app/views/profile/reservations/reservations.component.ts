import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/shared/services/auth/auth.service';
import { IReservation, ReservationsService } from 'src/shared/services/reservations/reservations.service';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.scss']
})
export class ReservationsComponent implements OnInit {
  reservations: IReservation[];
  userId: number = 1;

  constructor(
    private reservationsService: ReservationsService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    // const supplierId = this.authService.user?.supplierId;
    this.userId = this.authService.user!.sub;

    // if (supplierId) {
      this.reservationsService.getReservations(this.userId).subscribe(reservations => this.reservations = reservations);
    // }
  }

  delete(reservationId: number) {
    this.reservationsService.deleteReservation(this.userId, reservationId).subscribe(
      {
        next: (_) => this.reservations = this.reservations.filter(r => r.id != reservationId),
        error: (e)=> {
          if (e.status == 403) {
            const message = "Reservation cannot be deleted later than 24 hours before beginning. "
          }
        }
      });
  }

  viewDetails(reservationId: number) {
    
  }
}
