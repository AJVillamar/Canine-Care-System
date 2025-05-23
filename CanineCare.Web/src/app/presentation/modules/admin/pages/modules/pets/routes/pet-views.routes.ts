import { Routes } from '@angular/router';
import { PET_SUB_ROUTES } from './pet-subroutes.constants';

export const PetViews: Routes = [
    {
        path: '',
        children: [
            {
                path: '',
                redirectTo: PET_SUB_ROUTES[0].path,
                pathMatch: 'full'
            },
            ...PET_SUB_ROUTES.map(r => ({
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
