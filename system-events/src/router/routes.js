export const routes = [
    {
        name: 'home',
        path: '/',
        component: () => import('@src/views/HomeView.vue')
    },
    {
        name: 'login',
        path: '/login',
        component: () => import('@src/views/LoginView.vue')
    },
    {
        name: 'settings',
        path: '/settings',
        component: () => import('@src/views/SettingsView.vue')
    },
    {
        name: 'events',
        path: '/events',
        component: () => import('@src/views/EventsView.vue')
    }, 
    {
        name: '',
        path: '/exit',
        component: () => import('@src/views/HomeView.vue')
    }
]