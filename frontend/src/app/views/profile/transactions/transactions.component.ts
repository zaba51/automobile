import { Component } from '@angular/core';
import { map } from 'rxjs';
import { AuthService } from 'src/shared/services/auth/auth.service';
import { IReservationTransaction, SupplierService } from 'src/shared/services/supplier/supplier.service';

@Component({
  selector: 'app-transactions',
  templateUrl: './transactions.component.html',
  styleUrls: ['./transactions.component.scss']
})
export class TransactionsComponent {
  protected reservationTransactions: IReservationTransaction[] | 'Loading' | 'Error' = 'Loading';

  constructor(private supplierService: SupplierService, private authService: AuthService) {}

  ngOnInit() {
    if (this.authService.user?.supplierId) {
      this.supplierService
        .getMessagesForSupplier(this.authService.user?.supplierId)
        .pipe(
          map((messages) => messages.map(m => JSON.parse(m) as IReservationTransaction))
        ).subscribe({
          next: (reservations) => {
            this.reservationTransactions = reservations;
          },
          error: (error) => {
            this.reservationTransactions = 'Error';
            console.log(error);
          }
        }
      )
    }
  } 
}
