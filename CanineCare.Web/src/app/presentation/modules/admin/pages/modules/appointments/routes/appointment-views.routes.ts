import { Routes } from '@angular/router';
import { APPOINTMENT_SUB_ROUTES } from './appointment-subroutes.constants';

export const AppointmentViews: Routes = [
    {
        path: '',
        children: [
            {
                path: '',
                redirectTo: APPOINTMENT_SUB_ROUTES[0].path,
                pathMatch: 'full'
            },
            ...APPOINTMENT_SUB_ROUTES.map(r => ({
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
