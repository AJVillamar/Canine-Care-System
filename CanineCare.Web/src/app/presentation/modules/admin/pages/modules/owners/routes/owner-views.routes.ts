import { Routes } from '@angular/router';
import { OWNER_SUB_ROUTES } from './owner-subroutes.constants';

export const OwnerViews: Routes = [
    {
        path: '',
        children: [
            {
                path: '',
                redirectTo: OWNER_SUB_ROUTES[0].path,
                pathMatch: 'full'
            },
            ...OWNER_SUB_ROUTES.map(r => ({
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
