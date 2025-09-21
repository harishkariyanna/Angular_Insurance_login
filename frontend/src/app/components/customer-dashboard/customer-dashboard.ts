import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { PolicyService } from '../../services/policy';
import { AuthService } from '../../services/auth';
import { Policy } from '../../models/policy.model';

@Component({
  selector: 'app-customer-dashboard',
  imports: [CommonModule],
  templateUrl: './customer-dashboard.html',
  styleUrl: './customer-dashboard.css'
})
export class CustomerDashboard implements OnInit {
  policies: Policy[] = [];
  currentUser: any;
  assignedAgent: any = null;

  constructor(
    private policyService: PolicyService,
    private authService: AuthService,
    private router: Router,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
    this.loadPolicies();
    this.loadAgentInfo();
    
    // Refresh data when returning from policy application
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras?.state?.['refresh']) {
      setTimeout(() => this.refreshData(), 100);
    }
  }

  loadPolicies() {
    this.http.get<any[]>('https://localhost:7060/api/policies').subscribe({
      next: (policies) => {
        this.policies = policies.filter(p => p.customerId === this.currentUser.id);
      },
      error: (error) => {
        console.error('Error loading policies:', error);
        this.policies = [];
      }
    });
  }

  loadAgentInfo() {
    this.http.get<any>(`https://localhost:7060/api/Users/${this.currentUser.id}`).subscribe({
      next: (user) => {
        if (user.agentId) {
          this.http.get<any>(`https://localhost:7060/api/Users/${user.agentId}`).subscribe({
            next: (agent) => {
              this.assignedAgent = {
                id: agent.id,
                name: `${agent.firstName} ${agent.lastName}`,
                email: agent.email,
                phoneNumber: agent.phoneNumber
              };
            },
            error: () => this.assignedAgent = null
          });
        } else {
          this.assignedAgent = null;
        }
      },
      error: () => this.assignedAgent = null
    });
  }

  renewPolicy(policy: Policy) {
    if (policy.isExpired && policy.canBeRenewed) {
      const renewalData = {
        newStartDate: new Date().toISOString(),
        newEndDate: new Date(new Date().setFullYear(new Date().getFullYear() + 1)).toISOString(),
        notes: 'Policy renewal requested by customer'
      };
      
      this.http.post(`https://localhost:7060/api/policies/${policy.id}/renew`, renewalData).subscribe({
        next: (response) => {
          alert('Policy renewal request submitted successfully!');
          this.loadPolicies();
        },
        error: (error) => {
          console.error('Error renewing policy:', error);
          alert('Failed to renew policy. Please contact your agent.');
        }
      });
    }
  }

  viewPolicy(policy: Policy) {
    console.log('View policy:', policy.id);
  }

  applyForPolicy() {
    this.router.navigate(['/apply-policy']);
  }

  refreshData() {
    this.loadPolicies();
    this.loadAgentInfo();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
