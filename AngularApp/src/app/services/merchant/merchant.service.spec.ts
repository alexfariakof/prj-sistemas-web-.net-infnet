import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { Merchant } from 'src/app/model';
import { Address } from 'src/app/model';
import { MerchantService } from '..';

describe('MerchantService', () => {
  let service: MerchantService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MerchantService]
    });

    service = TestBed.inject(MerchantService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should create merchant via POST request', () => {
    // Arrange
    const mockAddress: Address = {
      zipcode: '99999-999',
      street: 'Rua Example',
      complement: '',
      neighborhood: 'Bairro Example',
      city: 'Cidade Example',
      state: 'Estado Example',
      country: 'BR'
    };
    const mockMerchant: Merchant = {
      name: "John Doe",
      email: "user@merchant.com",
      password: "12345",
      cpf: "123.456.789-01",
      cnpj: "12.345.678/0001-90",
      phone: "+5521992879319",
      address: mockAddress,
      flatId: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
      card: {
        number: '',
        validate: '',
        cvv: ''
      }
    };

    // Act
    service.create(mockMerchant).subscribe((response: any) => {
      // Assert
      expect(response).toBeTruthy();
    });

    // Assert
    const req = httpMock.expectOne('merchant');
    expect(req.request.method).toBe('POST');
    req.flush({});
  });
});
