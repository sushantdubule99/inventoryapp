import { TestBed } from '@angular/core/testing';

import { BillSrv } from './bill-srv';

describe('BillSrv', () => {
  let service: BillSrv;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BillSrv);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
