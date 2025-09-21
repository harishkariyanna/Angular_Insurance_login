import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CustomerService } from '../../services/customer';
import { PolicyService } from '../../services/policy';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-admin-dashboard',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css'
})
export class AdminDashboard implements OnInit {
  customers: any[] = [];
  agents: any[] = [];
  selectedTab = 'customers';
  selectedCustomer: any = null;
  assignAgentId: number = 0;
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
    this.loadCustomers();
    this.loadAgents();
  }

  loadCustomers() {
    this.http.get<any[]>('https://localhost:7060/api/Users').subscribe({
      next: (users) => {
        const customerUsers = users.filter(u => u.role?.name === 'Customer');
        this.customers = customerUsers.map(c => ({
          id: c.id,
          name: `${c.firstName} ${c.lastName}`,
          email: c.email,
          agentId: c.agentId || null
        }));
      },
      error: (error) => {
        console.error('Error loading customers:', error);
        this.customers = [];
      }
    });
  }

  loadAgents() {
    this.customerService.getAgents().subscribe({
      next: (agents) => {
        this.agents = agents.map(a => ({
          id: a.id,
          name: `${a.firstName} ${a.lastName}`,
          email: a.email
        }));
      },
      error: (error) => {
        console.error('Error loading agents:', error);
        this.agents = [];
      }
    });
  }

  selectCustomer(customer: any) {
    this.selectedCustomer = customer;
    this.assignAgentId = customer.agentId;
  }

  assignAgent() {
    if (this.selectedCustomer && this.assignAgentId && this.assignAgentId !== 0) {
      const newAgentId = Number(this.assignAgentId);
      const oldAgentId = this.selectedCustomer.agentId;
      
      const updateData = { agentId: newAgentId };
      
      this.http.put(`https://localhost:7060/api/Users/${this.selectedCustomer.id}`, updateData).subscribe({
        next: () => {
          // Update the selected customer
          this.selectedCustomer.agentId = newAgentId;
          
          // Update all customers in the list to ensure consistency
          this.customers = this.customers.map(customer => {
            if (customer.id === this.selectedCustomer.id) {
              return { ...customer, agentId: newAgentId };
            }
            return customer;
          });
          
          console.log(`Customer ${this.selectedCustomer.id} moved from agent ${oldAgentId} to agent ${newAgentId}`);
        },
        error: (error) => {
          console.error('Failed to assign agent:', error);
          // Fallback to local update
          this.selectedCustomer.agentId = newAgentId;
          const customerIndex = this.customers.findIndex(c => c.id === this.selectedCustomer.id);
          if (customerIndex !== -1) {
            this.customers[customerIndex] = { ...this.selectedCustomer };
          }
        }
      });
    }
  }

  editCustomer(customer: any) {
    console.log('Edit customer:', customer.id);
  }

  deleteCustomer(customer: any) {
    this.customers = this.customers.filter(c => c.id !== customer.id);
  }

  getCustomerCount(agentId: number): number {
    return this.customers.filter(c => c.agentId === agentId).length;
  }

  getAgentName(agentId: number): string {
    const agent = this.agents.find(a => a.id === agentId);
    return agent ? agent.name : 'Unassigned';
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
