import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CustomerService } from '../../services/customer';
import { PolicyService } from '../../services/policy';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-agent-dashboard',
  imports: [CommonModule],
  templateUrl: './agent-dashboard.html',
  styleUrl: './agent-dashboard.css'
})
export class AgentDashboard implements OnInit {
  customers: any[] = [];
  selectedCustomer: any = null;
  customerPolicies: any[] = [];
  currentUser: any;

  constructor(
    private customerService: CustomerService,
    private policyService: PolicyService,
    private authService: AuthService,
    private router: Router,
    private http: HttpClient
  ) {
    this.currentUser = this.authService.getCurrentUser();
  }

  ngOnInit() {
    this.currentUser = this.authService.getCurrentUser();
    this.loadCustomers();
  }

  refreshData() {
    this.loadCustomers();
    this.selectedCustomer = null;
    this.customerPolicies = [];
  }

  loadCustomers() {
    // Load customers from Users API and filter by agent assignment
    this.http.get<any[]>('https://localhost:7060/api/Users').subscribe({
      next: (users) => {
        const customerUsers = users.filter(u => 
          u.role?.name === 'Customer' && u.agentId === this.currentUser.id
        );
        
        // Load policies to get policy counts
        this.policyService.getPolicies().subscribe({
          next: (policies) => {
            this.customers = customerUsers.map(customer => {
              const customerPolicies = policies.filter(p => p.customerId === customer.id);
              return {
                id: customer.id,
                name: `${customer.firstName} ${customer.lastName}`,
                policyCount: customerPolicies.length
              };
            });
          },
          error: (error) => {
            this.customers = customerUsers.map(customer => ({
              id: customer.id,
              name: `${customer.firstName} ${customer.lastName}`,
              policyCount: 0
            }));
          }
        });
      },
      error: (error) => {
        console.error('Error loading customers:', error);
        this.customers = [];
      }
    });
  }

  selectCustomer(customer: any) {
    this.selectedCustomer = customer;
    this.policyService.getPolicies().subscribe(policies => {
      this.customerPolicies = policies.filter(p => p.customerId === customer.id);
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
