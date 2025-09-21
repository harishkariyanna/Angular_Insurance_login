import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { PolicyService } from '../../services/policy';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-policy-application',
  imports: [CommonModule, FormsModule],
  templateUrl: './policy-application.html',
  styleUrl: './policy-application.css'
})
export class PolicyApplication {
  applicationData = {
    policyType: '',
    startDate: '',
    endDate: '',
    premiumAmount: 0,
    coverageAmount: 0,
    deductible: 0,
    notes: ''
  };

  policyTypes = ['Health', 'Life', 'Vehicle', 'Home', 'Travel'];
  loading = false;
  currentUser: any;

  constructor(
    private policyService: PolicyService,
    private authService: AuthService,
    private router: Router,
    private http: HttpClient
  ) {
    this.currentUser = this.authService.getCurrentUser();
  }

  onSubmit() {
    this.loading = true;
    const policyData = {
      customerId: this.currentUser.id,
      policyType: this.applicationData.policyType,
      startDate: new Date(this.applicationData.startDate).toISOString(),
      endDate: new Date(this.applicationData.endDate).toISOString(),
      premiumAmount: Number(this.applicationData.premiumAmount),
      coverageAmount: Number(this.applicationData.coverageAmount),
      deductible: Number(this.applicationData.deductible),
      notes: this.applicationData.notes || ''
    };

    console.log('Sending policy data:', policyData);
    this.http.post('https://localhost:7060/api/policies', policyData).subscribe({
      next: (response) => {
        alert('Policy application submitted successfully!');
        this.loading = false;
        this.router.navigate(['/customer-dashboard'], { state: { refresh: true } });
      },
      error: (error) => {
        console.error('Error creating policy:', error);
        alert('Failed to submit policy application. Please try again.');
        this.loading = false;
      }
    });
  }

  goBack() {
    this.router.navigate(['/customer-dashboard']);
  }
}
