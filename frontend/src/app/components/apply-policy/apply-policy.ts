import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-apply-policy',
  imports: [CommonModule, FormsModule],
  templateUrl: './apply-policy.html',
  styleUrl: './apply-policy.css'
})
export class ApplyPolicy {
  policyApplication = {
    policyType: '',
    coverageAmount: 0,
    premiumAmount: 0,
    deductible: 0,
    notes: ''
  };
  
  currentUser: any;
  isSubmitting = false;

  constructor(
    private router: Router,
    private http: HttpClient,
    private authService: AuthService
  ) {
    this.currentUser = this.authService.getCurrentUser();
  }

  onPolicyTypeChange() {
    switch(this.policyApplication.policyType) {
      case 'Auto':
        this.policyApplication.coverageAmount = 50000;
        this.policyApplication.premiumAmount = 1200;
        this.policyApplication.deductible = 1000;
        break;
      case 'Home':
        this.policyApplication.coverageAmount = 200000;
        this.policyApplication.premiumAmount = 2400;
        this.policyApplication.deductible = 2000;
        break;
      case 'Life':
        this.policyApplication.coverageAmount = 500000;
        this.policyApplication.premiumAmount = 3600;
        this.policyApplication.deductible = 0;
        break;
      case 'Health':
        this.policyApplication.coverageAmount = 100000;
        this.policyApplication.premiumAmount = 4800;
        this.policyApplication.deductible = 5000;
        break;
    }
  }

  submitApplication() {
    if (!this.policyApplication.policyType) {
      alert('Please select a policy type');
      return;
    }

    this.isSubmitting = true;
    
    const policyData = {
      customerId: this.currentUser.id,
      agentId: null,
      policyType: this.policyApplication.policyType,
      startDate: new Date(),
      endDate: new Date(new Date().setFullYear(new Date().getFullYear() + 1)),
      premiumAmount: this.policyApplication.premiumAmount,
      coverageAmount: this.policyApplication.coverageAmount,
      deductible: this.policyApplication.deductible,
      notes: this.policyApplication.notes
    };

    this.http.post('https://localhost:7060/api/policies', policyData).subscribe({
      next: () => {
        alert('Policy application submitted successfully!');
        this.router.navigate(['/customer-dashboard']);
      },
      error: (error) => {
        console.error('Error submitting policy:', error);
        alert('Failed to submit policy application.');
        this.isSubmitting = false;
      }
    });
  }

  cancel() {
    this.router.navigate(['/customer-dashboard']);
  }
}