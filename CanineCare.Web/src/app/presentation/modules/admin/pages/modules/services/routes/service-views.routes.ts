import { Routes } from '@angular/router';
import { SERVICE_SUB_ROUTES } from './service-subroutes.constants';

export const ServiceViews: Routes = [
    {
        path: '',
        children: [
            {
                path: '',
                redirectTo: SERVICE_SUB_ROUTES[0].path,
                pathMatch: 'full'
            },
            ...SERVICE_SUB_ROUTES.map(r => ({
                path: r.path,
                title: r.title,
                data: {
                    label: r.label,
                    icon: r.icon
                },
                loadComponent: r.loadComponent
            }))
        ]
    }
];
