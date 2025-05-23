export const APPOINTMENT_SUB_ROUTES = [
    {
        path: 'create',
        title: 'Registrar Cita | Sistema Atención Canina',
        label: 'Crear',
        icon: 'fa-solid fa-calendar-plus',
        loadComponent: () => import('../views/appointment-create/appointment-create.component').then(c => c.AppointmentCreateComponent)
    },
    {
        path: 'search',
        title: 'Buscar Citas | Sistema Atención Canina',
        label: 'Buscar',
        icon: 'fa-brands fa-searchengin',
        loadComponent: () => import('../views/appointment-search/appointment-search.component').then(c => c.AppointmentSearchComponent)
    }
];
