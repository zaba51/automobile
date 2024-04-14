import { Injectable } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { PaymentDialogComponent } from 'src/shared/components/payment-dialog/payment-dialog.component';

export interface DialogData {
  price: number;
}

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(public dialog: MatDialog) { }

  processPayment(price: number, userId: number): Promise<boolean> {
    return new Promise((resolve) => {
      const dialogRef = this.dialog.open(PaymentDialogComponent, {
        data: { price },
        panelClass: 'payment-dialog'
      });


      dialogRef.afterClosed().subscribe(result => {
          resolve(result);       
      });
    });
  }
}
