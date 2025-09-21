import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login';
import { Register } from './components/register/register';
import { DashboardComponent } from './components/dashboard/dashboard';
import { CustomerDashboard } from './components/customer-dashboard/customer-dashboard';
import { AgentDashboard } from './components/agent-dashboard/agent-dashboard';
import { AdminDashboard } from './components/admin-dashboard/admin-dashboard';
import { PolicyApplication } from './components/policy-application/policy-application';
import { authGuard } from './guards/auth-guard';
import { roleGuard } from './guards/role-guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: Register },
  { 
    path: 'customer-dashboard', 
    component: CustomerDashboard, 
    canActivate: [authGuard, roleGuard],
    data: { roles: ['Customer'] }
  },
  { 
    path: 'agent-dashboard', 
    component: AgentDashboard, 
    canActivate: [authGuard, roleGuard],
    data: { roles: ['Agent'] }
  },
  { 
    path: 'admin-dashboard', 
    component: AdminDashboard, 
    canActivate: [authGuard, roleGuard],
    data: { roles: ['Admin'] }
  },
  { 
    path: 'apply-policy', 
    component: PolicyApplication, 
    canActivate: [authGuard, roleGuard],
    data: { roles: ['Customer'] }
  },
  { 
    path: 'dashboard', 
    component: DashboardComponent, 
    canActivate: [authGuard] 
  },
  { path: '**', redirectTo: '/login' }
];