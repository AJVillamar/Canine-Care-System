import { Routes } from '@angular/router';
import { APP_ROUTES_MODULES } from './routes.constants';
import { accessGuard } from '@presentation/shared/guards/access.guard';
import { APP_ROLES } from './role.constants';

export const routes: Routes = [
    {
        path: '',
        redirectTo: APP_ROUTES_MODULES.MODULE_AUTHENTICATION,
        pathMatch: 'full'
    },
    {
        path: APP_ROUTES_MODULES.MODULE_AUTHENTICATION,
        title: 'Autenticación | Sistema Atención Canina',
        loadComponent: () => import('../modules/auth/auth.component').then(c => c.AuthComponent),
        loadChildren: () => import('../modules/auth/routes/auth-pages.routes').then(r => r.AuthPages)
    },
    {
        path: APP_ROUTES_MODULES.MODULE_ADMINISTRATION,
        title: 'Administrador | Sistema Atención Canina',
        canActivate: [accessGuard([APP_ROLES.ADMINISTRATOR, APP_ROLES.CLIENT, APP_ROLES.PROFESSIONAL])],
        loadComponent: () => import('../modules/admin/pages/home/home.component').then(c => c.HomeComponent),
        loadChildren: () => import('../modules/admin/routes/admin-pages.routes').then(r => r.AdminPages)
    }
];
