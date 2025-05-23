export const DASHBOARD_SUB_ROUTES = [
    {
        path: 'overview',
        title: 'Resumen General | Sistema Atención Canina',
        label: 'Resumen',
        icon: 'fa-solid fa-chart-pie',
        loadComponent: () => import('../views/dashboard-overview/dashboard-overview.component').then(c => c.DashboardOverviewComponent)
    }
];
