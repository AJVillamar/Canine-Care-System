import { Routes } from '@angular/router';
import { APP_ROUTES_ADMIN } from './routes-admin-constants';

import { APP_ROLES } from '@presentation/router/role.constants';
import { accessGuard } from '@presentation/shared/guards/access.guard';

import { PET_SUB_ROUTES } from '../pages/modules/pets/routes/pet-subroutes.constants';
import { OWNER_SUB_ROUTES } from '../pages/modules/owners/routes/owner-subroutes.constants';
import { SERVICE_SUB_ROUTES } from '../pages/modules/services/routes/service-subroutes.constants';
import { DASHBOARD_SUB_ROUTES } from '../pages/modules/dashboard/routes/dashboard-subroutes.constants';
import { APPOINTMENT_SUB_ROUTES } from '../pages/modules/appointments/routes/appointment-subroutes.constants';
import { PROFESSIONAL_SUB_ROUTES } from '../pages/modules/professionals/routes/professional-subroutes.constants';

export const AdminPages: Routes = [
    {
        path: '',
        children: [
            {
                path: '',
                redirectTo: APP_ROUTES_ADMIN.ADMIN_DASHBOARD,
                pathMatch: 'full'
            },
            {
                path: APP_ROUTES_ADMIN.ADMIN_DASHBOARD,
                title: 'Dashboard | Sistema Atención Canina',
                canActivate: [accessGuard([APP_ROLES.ADMINISTRATOR, APP_ROLES.PROFESSIONAL])],
                data: {
                    icon: 'fa-solid fa-house',
                    label: 'Inicio',
                    children: DASHBOARD_SUB_ROUTES,
                    roles: [APP_ROLES.ADMINISTRATOR, APP_ROLES.CLIENT, APP_ROLES.PROFESSIONAL]
                },
                loadComponent: () => import('../pages/modules/dashboard/dashboard-home.component').then(c => c.DashboardHomeComponent),
                loadChildren: () => import('../pages/modules/dashboard/routes/dashboard-views.routes').then(c => c.DashboardView),
            },
            {
                path: APP_ROUTES_ADMIN.ADMIN_PROFESSIONAL,
                title: 'Profesionales | Sistema Atención Canina',
                canActivate: [accessGuard([APP_ROLES.ADMINISTRATOR])],
                data: {
                    icon: 'fa-solid fa-user-tie',
                    label: 'Profesionales',
                    children: PROFESSIONAL_SUB_ROUTES,
                    roles: [APP_ROLES.ADMINISTRATOR]
                },
                loadComponent: () => import('../pages/modules/professionals/professional-home.component').then(c => c.ProfessionalHomeComponent),
                loadChildren: () => import('../pages/modules/professionals/routes/professional-views.routes').then(c => c.ProfessionalViews),
            },
            {
                path: APP_ROUTES_ADMIN.ADMIN_OWNER,
                title: 'Dueños de Mascotas | Sistema Atención Canina',
                canActivate: [accessGuard([APP_ROLES.ADMINISTRATOR])],
                data: {
                    icon: 'fa-solid fa-users',
                    label: 'Clientes',
                    children: OWNER_SUB_ROUTES,
                    roles: [APP_ROLES.ADMINISTRATOR]
                },
                loadComponent: () => import('../pages/modules/owners/owner-home.component').then(c => c.OwnerHomeComponent),
                loadChildren: () => import('../pages/modules/owners/routes/owner-views.routes').then(c => c.OwnerViews),
            },
            {
                path: APP_ROUTES_ADMIN.ADMIN_PET,
                title: 'Mascotas | Sistema Atención Canina',
                canActivate: [accessGuard([APP_ROLES.ADMINISTRATOR])],
                data: {
                    icon: 'fa-solid fa-dog',
                    label: 'Mascotas',
                    children: PET_SUB_ROUTES,
                    roles: [APP_ROLES.ADMINISTRATOR]
                },
                loadComponent: () => import('../pages/modules/pets/pet-home.component').then(c => c.PetHomeComponent),
                loadChildren: () => import('../pages/modules/pets/routes/pet-views.routes').then(c => c.PetViews),
            },
            {
                path: APP_ROUTES_ADMIN.ADMIN_SERVICE,
                title: 'Servicios | Sistema Atención Canina',
                canActivate: [accessGuard([APP_ROLES.ADMINISTRATOR])],
                data: {
                    icon: 'fa-solid fa-shower',
                    label: 'Servicios',
                    children: SERVICE_SUB_ROUTES,
                    roles: [APP_ROLES.ADMINISTRATOR]
                },
                loadComponent: () => import('../pages/modules/services/service-home.component').then(c => c.ServiceHomeComponent),
                loadChildren: () => import('../pages/modules/services/routes/service-views.routes').then(c => c.ServiceViews),
            },
            {
                path: APP_ROUTES_ADMIN.ADMIN_APPOINTMENT,
                title: 'Citas | Sistema Atención Canina',
                canActivate: [accessGuard([APP_ROLES.ADMINISTRATOR])],
                data: {
                    icon: 'fa-solid fa-calendar-days',
                    label: 'Citas',
                    children: APPOINTMENT_SUB_ROUTES,
                    roles: [APP_ROLES.ADMINISTRATOR]
                },
                loadComponent: () => import('../pages/modules/appointments/appointment-home.component').then(c => c.AppointmentHomeComponent),
                loadChildren: () => import('../pages/modules/appointments/routes/appointment-views.routes').then(c => c.AppointmentViews),
            }
        ]
    }
]
