import { TestBed } from '@angular/core/testing';

import { EmployerGuard } from './employer.guard';

describe('EmployeeGuard', () => {
  let guard: EmployerGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(EmployerGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
