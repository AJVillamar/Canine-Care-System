export const SERVICE_SUB_ROUTES = [
    {
        path: 'create',
        title: 'Crear Servicio | Sistema Atención Canina',
        label: 'Crear',
        icon: 'fa-solid fa-file-circle-plus',
        loadComponent: () => import('../views/service-create/service-create.component').then(c => c.ServiceCreateComponent)
    },
    {
        path: 'list',
        title: 'Lista de Servicios | Sistema Atención Canina',
        label: 'Lista',
        icon: 'fa-solid fa-clipboard-list',
        loadComponent: () => import('../views/service-list/service-list.component').then(c => c.ServiceListComponent)
    },
];
