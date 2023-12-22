import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { tap } from 'rxjs';
import { AuthService } from 'src/shared/services/auth/auth.service';

export const isAuthenticatedGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  let isAuthenticated;

  authService.isLoggedIn$.subscribe(isLoggedIn => isAuthenticated = isLoggedIn);
  
  console.log(isAuthenticated);
  if (isAuthenticated) {
    return true;
  }
  else {
    router.navigate(['/login'])
    return false;
  }
};
