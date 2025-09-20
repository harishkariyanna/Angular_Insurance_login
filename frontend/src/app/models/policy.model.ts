export interface Policy {
  id: number;
  policyNumber: string;
  customerId: number;
  customerName: string;
  agentId: number;
  agentName: string;
  policyType: string;
  startDate: Date;
  endDate: Date;
  status: string;
  premiumAmount: number;
  coverageAmount: number;
  deductible: number;
  notes?: string;
  createdDate: Date;
}

export interface CreatePolicyRequest {
  customerId: number;
  policyType: string;
  startDate: Date;
  endDate: Date;
  premiumAmount: number;
  coverageAmount: number;
  deductible: number;
  notes?: string;
}