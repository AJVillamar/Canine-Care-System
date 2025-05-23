import { Routes } from '@angular/router';
import { DASHBOARD_SUB_ROUTES } from './dashboard-subroutes.constants';

export const DashboardView: Routes = [
    {
        path: '',
        children: [
            {
                path: '',
                redirectTo: DASHBOARD_SUB_ROUTES[0].path,
                pathMatch: 'full'
            },
            ...DASHBOARD_SUB_ROUTES.map(route => ({
                path: route.path,
                title: route.title,
                data: {
                    label: route.label,
                    icon: route.icon
                },
                loadComponent: route.loadComponent
            }))
        ]
    }
];
