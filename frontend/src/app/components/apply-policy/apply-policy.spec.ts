import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplyPolicy } from './apply-policy';

describe('ApplyPolicy', () => {
  let component: ApplyPolicy;
  let fixture: ComponentFixture<ApplyPolicy>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApplyPolicy]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplyPolicy);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
