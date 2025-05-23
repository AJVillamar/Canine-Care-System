import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import type { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';

import { ToastService } from '@infrastructure/common/toast.service';
import { TokenService } from '@infrastructure/common/token.service';
import { APP_ROUTES_AUTH } from '@presentation/modules/auth/routes/routes-auth-constants';

export const TokenInterceptor: HttpInterceptorFn = (req, next) => {

    const _router = inject(Router);
    const _token = inject(TokenService);
    const _toast = inject(ToastService);

    const token = _token.get();
    const login_url = APP_ROUTES_AUTH.AUTH_LOGIN;

    if (token) {
        if (_token.isTokenExpired()) {
            _token.remove();
            _router.navigate([login_url]);
            _toast.showError('Su sesión ha expirado. Por favor, inicie sesión nuevamente.');
        }
    }

    let clonedRequest = req;
    if (token) {
        clonedRequest = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`,
                ...(req.body ? { 'Content-Type': 'application/json' } : {})
            }
        });
    }

    return next(clonedRequest).pipe(
        catchError((error: HttpErrorResponse) => {
            if (error.status === 403) { // Maneja el error de acceso denegado
                _router.navigate([login_url]);
                _toast.showError('Inicie sesión, no autorizado.');
            }
            return throwError(() => new Error(error.message));
        })
    );
};
