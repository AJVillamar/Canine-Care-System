export const PROFESSIONAL_SUB_ROUTES = [
    {
        path: 'create',
        title: 'Crear Profesional | Sistema Atención Canina',
        label: 'Crear',
        icon: 'fa-solid fa-user-plus',
        loadComponent: () => import('../views/professional-create/professional-create.component').then(c => c.ProfessionalCreateComponent)
    },
    {
        path: 'list',
        title: 'Lista de Profesionales | Sistema Atención Canina',
        label: 'Lista',
        icon: 'fa-solid fa-users',
        loadComponent: () => import('../views/professional-list/professional-list.component').then(c => c.ProfessionalListComponent)
    },
];
