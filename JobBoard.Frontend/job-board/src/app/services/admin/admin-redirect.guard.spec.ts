import { TestBed } from '@angular/core/testing';

import { AdminRedirectGuard } from './admin-redirect.guard';

describe('AdminRedirectGuard', () => {
  let guard: AdminRedirectGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AdminRedirectGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
