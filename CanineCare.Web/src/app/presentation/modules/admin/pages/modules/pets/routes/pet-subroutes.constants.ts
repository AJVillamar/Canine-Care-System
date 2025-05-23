export const PET_SUB_ROUTES = [
    {
        path: 'create',
        title: 'Registrar Mascota | Sistema Atención Canina',
        label: 'Registrar',
        icon: 'fa-solid fa-circle-plus',
        loadComponent: () => import('../views/pet-create/pet-create.component').then(c => c.PetCreateComponent)
    },
    {
        path: 'edit',
        title: 'Editar Mascota | Sistema Atención Canina',
        label: 'Editar',
        icon: 'fa-solid fa-pen',
        loadComponent: () => import('../views/pet-edit/pet-edit.component').then(c => c.PetEditComponent)
    },
    {
        path: 'list',
        title: 'Lista de Mascota | Sistema Atención Canina',
        label: 'Lista',
        icon: 'fa-solid fa-clipboard-check',
        loadComponent: () => import('../views/pet-list/pet-list.component').then(c => c.PetListComponent)
    },
];
