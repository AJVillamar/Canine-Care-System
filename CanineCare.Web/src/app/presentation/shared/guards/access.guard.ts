import { inject } from '@angular/core';
import { Router, type CanActivateFn } from '@angular/router';

import { ToastService } from '@infrastructure/common/toast.service';
import { TokenService } from '@infrastructure/common/token.service';

import { APP_ROUTES_MODULES } from '@presentation/router/routes.constants';
import { APP_ROUTES_AUTH } from '@presentation/modules/auth/routes/routes-auth-constants';

export const accessGuard = (allowedRoles: string[]): CanActivateFn => {

  return (route, state) => {
    const router = inject(Router);
    const token = inject(TokenService);
    const toast = inject(ToastService);

    const redirectToLogin = (message: string) => {
      toast.showError(message, "Lo sentimos");
      router.navigate([APP_ROUTES_MODULES.MODULE_AUTHENTICATION, APP_ROUTES_AUTH.AUTH_LOGIN]);
      return false;
    };

    const user = token.getLoggedUser();
    if (!user) return redirectToLogin('Acceso denegado');
    if (token.isTokenExpired()) {
      token.remove();
      return redirectToLogin('Su sesión ha expirado. Por favor, inicie sesión nuevamente.');
    }

    const userRoles = (Array.isArray(user.role) ? user.role : [user.role])
      .map(role => role.toLowerCase());

    const hasAccess = allowedRoles.some(role => userRoles.includes(role.toLowerCase()));

    return hasAccess
      ? true
      : redirectToLogin('No cuenta con los permisos suficientes, por seguridad vuelva a iniciar sesión.');
  }

};

