import { TestBed } from '@angular/core/testing';

import { SystemAdministratorGuard } from './system-administrator.guard';

describe('SystemAdministratorGuard', () => {
  let guard: SystemAdministratorGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(SystemAdministratorGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
