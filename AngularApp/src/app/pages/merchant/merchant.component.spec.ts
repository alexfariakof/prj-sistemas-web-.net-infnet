import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { MerchantComponent } from './merchant.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { AddressService, MerchantService } from 'src/app/services';
import { Address, Merchant } from 'src/app/model';
import { of } from 'rxjs';

describe('MerchantComponent', () => {
  let component: MerchantComponent;
  let fixture: ComponentFixture<MerchantComponent>;
  let merchantService: MerchantService;
  let addressService: AddressService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, MerchantComponent, BrowserAnimationsModule, RouterTestingModule],
      providers:[AddressService]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MerchantComponent);
    component = fixture.componentInstance;
    merchantService = TestBed.inject(MerchantService);
    addressService = TestBed.inject(AddressService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('onSaveClick should call Merchant service Create method', fakeAsync(() => {
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
    spyOn(merchantService, 'create').and.returnValue(of(mockMerchant));
    spyOn(component, 'onSaveClick').and.callThrough();
    spyOn(component.router, 'navigate');

    // Act
    component.createAccountForm.patchValue(mockMerchant);
    component.onSaveClick();
    tick();

    // Assert
    expect(merchantService.create).toHaveBeenCalled();
    expect(merchantService.create).toHaveBeenCalledWith(mockMerchant);
    expect(component.router.navigate).toHaveBeenCalledWith(['/login']);
  }));

  it('buscarCEP should call address service buscarCep method', fakeAsync(() => {
    // Arrange
    const cep = '12345678';
    const mockAddress: Address = {
      zipcode: cep,
      street: 'Rua Example',
      complement: '',
      neighborhood: 'Bairro Example',
      city: 'Cidade Example',
      state: 'Estado Example',
      country: 'BR'
    };

    spyOn(addressService, 'buscarCep').and.returnValue(of(mockAddress));

    // Act
    component.buscarCEP();
    tick();

    // Assert
    expect(addressService.buscarCep).toHaveBeenCalled();
  }));

  it('onTooglePassword should toggle showPassword property and change eye icon class', () => {
    // Arrange
    const initialShowPasswordValue = component.showPassword;
    const initialEyeIconClassValue = component.eyeIconClass;

    // Act
    component.onTooglePassword();

    // Assert
    expect(component.showPassword).toBe(!initialShowPasswordValue);
    expect(component.eyeIconClass).not.toBe(initialEyeIconClassValue);
  });

  it('onToogleConfirmPassword should toggle showConfirmPassword property and change eye icon class', () => {
    // Arrange
    const initialShowConfirmPasswordValue = component.showConfirmPassword;
    const initialEyeIconClassConfirmPasswordValue = component.eyeIconClassConfirmPassword;

    // Act
    component.onToogleConfirmPassword();

    // Assert
    expect(component.showConfirmPassword).toBe(!initialShowConfirmPasswordValue);
    expect(component.eyeIconClassConfirmPassword).not.toBe(initialEyeIconClassConfirmPasswordValue);
  });

  it('isPasswordValid should return true if password and confirm password are not the same', () => {
    // Arrange
    component.createAccountForm.controls['password'].setValue('password1');
    component.createAccountForm.controls['confirmPassword'].setValue('password2');

    // Act
    const result = component.isPasswordValid();

    // Assert
    expect(result).toBeTrue();
  });

  it('isPasswordValid should return false if password and confirm password are the same', () => {
    // Arrange
    const password = 'password';
    component.createAccountForm.controls['password'].setValue(password);
    component.createAccountForm.controls['confirmPassword'].setValue(password);

    // Act
    const result = component.isPasswordValid();

    // Assert
    expect(result).toBeFalse();
  });
});
