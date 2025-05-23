export const OWNER_SUB_ROUTES = [
    {
        path: 'create',
        title: 'Registrar Dueño | Sistema Atención Canina',
        label: 'Crear',
        icon: 'fa-solid fa-user-plus',
        loadComponent: () => import('../views/owner-create/owner-create.component').then(c => c.OwnerCreateComponent)
    },
    {
        path: 'edit',
        title: 'Editar Dueño | Sistema Atención Canina',
        label: 'Editar',
        icon: 'fa-solid fa-user-pen',
        loadComponent: () => import('../views/owner-edit/owner-edit.component').then(c => c.OwnerEditComponent)
    },
    {
        path: 'list',
        title: 'Lista de Dueños | Sistema Atención Canina',
        label: 'Lista',
        icon: 'fa-solid fa-users',
        loadComponent: () => import('../views/owner-list/owner-list.component').then(c => c.OwnerListComponent)
    }
];
