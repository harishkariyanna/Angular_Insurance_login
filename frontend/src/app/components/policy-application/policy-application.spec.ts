import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PolicyApplication } from './policy-application';

describe('PolicyApplication', () => {
  let component: PolicyApplication;
  let fixture: ComponentFixture<PolicyApplication>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PolicyApplication]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PolicyApplication);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
