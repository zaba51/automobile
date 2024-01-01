import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from 'src/shared/services/auth/auth.service';

export const isSupplierGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.user?.role === 'supplier') {
    return true;
  }
  else {
    router.navigate(['/'])
    return false;
  }
};
