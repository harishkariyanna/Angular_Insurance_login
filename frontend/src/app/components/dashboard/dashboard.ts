import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth';
import { PolicyService } from '../../services/policy';
import { Policy } from '../../models/policy.model';
import { User } from '../../models/auth.model';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class DashboardComponent implements OnInit {
  currentUser: User | null = null;
  policies: Policy[] = [];
  loading = true;

  constructor(
    private authService: AuthService,
    private policyService: PolicyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
    
    this.loadPolicies();
  }

  loadPolicies(): void {
    this.policyService.getPolicies().subscribe({
      next: (policies) => {
        this.policies = policies;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  canCreatePolicy(): boolean {
    return this.authService.hasRole('Admin') || this.authService.hasRole('Agent');
  }

  canDeletePolicy(): boolean {
    return this.authService.hasRole('Admin');
  }
}
