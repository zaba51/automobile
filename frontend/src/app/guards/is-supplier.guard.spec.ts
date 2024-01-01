import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { isSupplierGuard } from './is-supplier.guard';

describe('isSupplierGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => isSupplierGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
