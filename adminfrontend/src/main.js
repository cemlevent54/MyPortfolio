import './assets/main.css'

import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'

import Users from './ui/UserPages/Users.vue';
import HomePage from './ui/HomePage/HomePage.vue';
import Projects from './ui/ProjectPages/Projects.vue';
import Technologies from './ui/TechnologyPages/Technologies.vue';
import Languages from './ui/LanguagePages/Languages.vue';
import Talents from './ui/TalentPages/Talents.vue';
import Hobbies from './ui/HobbyPages/Hobbies.vue';
import Educations from './ui/EducationPages/Educations.vue';
import Experiences from './ui/ExperiencePages/Experiences.vue';
// import SocialMedias from './ui/SocialMedia/SocialMedias.vue';
import MyAccount from './ui/AccountPages/MyAccount.vue';
import SignInPage from './ui/AccountPages/SignInPage.vue';
import RegisterPage from './ui/AccountPages/RegisterPage.vue';
// import SocialMediaNames from './ui/SocialMedia/SocialMediaNames.vue';
import TalentCategories from './ui/TalentPages/TalentCategories.vue';
import ProjectDetails from './ui/ProjectPages/ProjectDetails.vue';
import UserDetails from './ui/UserPages/UserDetails.vue';
import Roles from './ui/RolePages/Roles.vue';
import EducationCategory from './ui/EducationPages/EducationCategory.vue';
import ProjectCategory from './ui/ProjectPages/ProjectCategory.vue';

const routes = [
    { path: '/', redirect: (to) => localStorage.getItem('isLoggedIn') === 'true' ? '/home' : '/login' },
    { path: '/home', component: HomePage, meta: { requiresAuth: true } },
    { path: '/users', component: Users, meta: { requiresAuth: true } },
    {
        path: '/user/:userName', // Route parametresi tanımlı mı?
        component: UserDetails, // UserDetails.vue dosyanız olmalı
        props: true
    },
    { path: '/roles', component: Roles, meta: { requiresAuth: true } },
    { path: '/projects', component: Projects, meta: { requiresAuth: true } },
    { path: '/projects/:id', component: ProjectDetails, meta: { requiresAuth: true } },
    { path: '/projects/categories', component: ProjectCategory, meta: { requiresAuth: true } },
    
    { path: '/technologies', component: Technologies, meta: { requiresAuth: true } },
    
    { path: '/languages', component: Languages, meta: { requiresAuth: true } },
    { path: '/talents', component: Talents, meta: { requiresAuth: true } },
    { path: '/hobbies', component: Hobbies, meta: { requiresAuth: true } },
    { path: '/educations', component: Educations, meta: { requiresAuth: true } },
    { path: '/educations/educationcategories', component: EducationCategory, meta: { requiresAuth: true }},
    { path: '/experiences', component: Experiences, meta: { requiresAuth: true } },
    // { path: '/socialmedias', component: SocialMedias, meta: { requiresAuth: true } },
    { path: '/myaccount', component: MyAccount, meta: { requiresAuth: true } },
    // { path: '/socialmediaplatform', component: SocialMediaNames, meta: { requiresAuth: true } },
    { path: '/talentcategories', component: TalentCategories, meta: { requiresAuth: true } },
    
    // Giriş ve Kayıt sayfaları auth gerektirmez
    { path: '/login', component: SignInPage },
    { path: '/register', component: RegisterPage },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

// 🚀 **Auth Guard: Giriş kontrolü**
router.beforeEach((to, from, next) => {
    const isAuthenticated = localStorage.getItem('isLoggedIn') === 'true';

    if (to.meta.requiresAuth && !isAuthenticated) {
        next('/login'); // Giriş yapmamış kullanıcıyı login sayfasına yönlendir
    } else if ((to.path === '/login' || to.path === '/register') && isAuthenticated) {
        next('/home'); // Zaten giriş yapmış kullanıcı login/register sayfasına gidemez
    } else {
        next(); // Diğer sayfalara yönlendir
    }
});

const app = createApp(App);
app.use(router);
app.mount('#app');
