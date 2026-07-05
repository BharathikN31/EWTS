import { Routes } from '@angular/router';
import { Login } from './features/auth/login/login';
import { Register } from './features/auth/register/register';
import { Dashboard } from './features/dashboard/dashboard';
import { Tasks } from './features/tasks/tasks';
import { TaskDetail } from './features/tasks/task-detail/task-detail';
import { Workflow } from './features/workflow/workflow';
import { CreateUser } from './features/admin/create-user/create-user';
import { authGuard } from './core/guards/auth-guard';
import { roleGuard } from './core/guards/role-guard';
import { Profile } from './features/profile/profile';



export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: 'dashboard', component: Dashboard, canActivate: [authGuard] },
   { path: 'profile', component: Profile, canActivate: [authGuard] },
  { path: 'tasks', component: Tasks, canActivate: [authGuard] },
  { path: 'tasks/:id', component: TaskDetail, canActivate: [authGuard] },
  {
    path: 'workflow',
    component: Workflow,
    canActivate: [authGuard, roleGuard(['Manager', 'Admin'])]
  },
  {
    path: 'admin/create-user',
    component: CreateUser,
    canActivate: [authGuard, roleGuard(['Admin'])]
  }
];