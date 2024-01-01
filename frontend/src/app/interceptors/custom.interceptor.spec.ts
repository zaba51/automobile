import { TestBed } from '@angular/core/testing';

import { CustomInterceptor } from './custom.interceptor';

describe('CustomInterceptorInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      CustomInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: CustomInterceptor = TestBed.inject(CustomInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
