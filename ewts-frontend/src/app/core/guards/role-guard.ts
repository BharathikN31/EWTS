import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Auth } from '../services/auth';

export function roleGuard(allowedRoles: string[]): CanActivateFn {
  return () => {
    const authService = inject(Auth);
    const router = inject(Router);

    const role = authService.getRoleFromToken();

    if (role && allowedRoles.includes(role)) {
      return true;
    }

    router.navigate(['/dashboard']);
    return false;
  };
}