import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { ToastService } from '@infrastructure/common/toast.service';
import type { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';

export const ErrorInterceptor: HttpInterceptorFn = (req, next) => {

    const _router = inject(Router);
    const _toast = inject(ToastService);

    return next(req).pipe(
        catchError((err: HttpErrorResponse) => {
            let message = 'Ocurrió un problema inesperado.';

            if (err.status !== 200 && err.status !== 201) {
                switch (err.status) {
                    case 400: // Petición incorrecta
                    case 401: // No autorizado
                    case 404: // No encontrado
                    case 409: // Conflicto
                    case 422: // Error de validación
                        message = err.error?.Message;
                        break;

                    case 500: // Error interno del servidor
                        message = 'Error interno del servidor: Por favor, intenta nuevamente más tarde';
                        break;

                    default: // Cualquier otro código de error no manejado
                        message = err.error?.Message || 'Ha ocurrido un error inesperado';
                        break;
                }
            }

            _toast.showError(message, "Atención");
            return throwError(() => message);
        })
    );
};
