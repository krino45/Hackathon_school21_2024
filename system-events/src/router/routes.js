export const routes = [
    {
        name: 'Home',
        path: '/',
        component: () => import('@src/views/HomeView.vue')
    },
    {
        name: 'Login',
        path: '/login',
        component: () => import('@src/views/LoginView.vue')
    },
    {
        name: 'Settings',
        path: '/settings',
        component: () => import('@src/views/SettingsView.vue')
    },
    {
        name: 'Events',
        path: '/events',
        component: () => import('@src/views/EventsView.vue')
    }
]