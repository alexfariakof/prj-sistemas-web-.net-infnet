import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CustomerService } from './customer.service';
import { Customer } from 'src/app/model';

describe('CustomerService', () => {
  let service: CustomerService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CustomerService]
    });

    service = TestBed.inject(CustomerService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should create customer via POST request', () => {
    // Arrange
    const mockCustomer: Customer = {
      name: 'John Doe',
      email: 'john@example.com',
      password: 'password123',
      cpf: '12345678901',
      birth: '1990-01-01',
      phone: '123456789',
      address: {
        zipcode: '12345-678',
        street: 'Main St',
        number: '123',
        neighborhood: 'Downtown',
        city: 'City',
        state: 'State'
      },
      flatId: '',
      card: {}
    };

    // Act
    service.create(mockCustomer).subscribe((response: any) => {
      // Assert
      expect(response).toBeTruthy();
    });

    // Assert
    const req = httpMock.expectOne('customer');
    expect(req.request.method).toBe('POST');
    req.flush({});
  });
});
