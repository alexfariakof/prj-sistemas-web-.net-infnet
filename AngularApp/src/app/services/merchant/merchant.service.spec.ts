import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import { HttpTestingController, provideHttpClientTesting } from "@angular/common/http/testing";
import { TestBed, inject } from "@angular/core/testing";
import { CustomInterceptor } from "../../interceptors/http.interceptor.service";
import { Address, Merchant } from "../../model";
import { MerchantService } from "..";

describe('MerchantService', () => {
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [MerchantService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', inject([MerchantService], (service: MerchantService) => {
    // Assert
    expect(service).toBeTruthy();
  }));

  it('should send a post request to the api/merchant endpoint', inject(
    [MerchantService, HttpTestingController],
    (service: MerchantService, httpMock: HttpTestingController) => {
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
    const mockResponse: Merchant = {
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

      service.create(mockResponse).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'api/merchant';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('POST');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

});

