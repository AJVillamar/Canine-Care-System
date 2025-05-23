import { Routes } from '@angular/router';
import { APP_ROUTES_AUTH } from './routes-auth-constants';

export const AuthPages: Routes = [
    {
        path: '',
        children: [
            {
                path: '',
                redirectTo: APP_ROUTES_AUTH.AUTH_LOGIN,
                pathMatch: 'full'
            },
            {
                path: APP_ROUTES_AUTH.AUTH_LOGIN,
                title: 'Iniciar Sesión | Sistema Atención Canina',
                data: {
                    icon: 'fa-solid fa-right-to-bracket',
                    label: 'Iniciar Sesión',
                },
                loadComponent: () => import('../pages/auth-login/auth-login.component').then(c => c.AuthLoginComponent),
            }
        ]
    }
]