import { Routes } from '@angular/router';
import { PROFESSIONAL_SUB_ROUTES } from './professional-subroutes.constants';

export const ProfessionalViews: Routes = [
    {
        path: '',
        children: [
            {
                path: '',
                redirectTo: PROFESSIONAL_SUB_ROUTES[0].path,
                pathMatch: 'full'
            },
            ...PROFESSIONAL_SUB_ROUTES.map(r => ({
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
