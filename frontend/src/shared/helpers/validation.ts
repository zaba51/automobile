import { FormGroup } from "@angular/forms";

export function markFormGroupTouched(formGroup: FormGroup) {
    Object.values(formGroup.controls).forEach(control => {
      if (control instanceof FormGroup) {
        markFormGroupTouched(control);
      } else {
        control.markAsTouched();
      }
    })
}