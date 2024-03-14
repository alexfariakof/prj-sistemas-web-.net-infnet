import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { Merchant, Address } from 'src/app/model';
import { MerchantService, AddressService } from 'src/app/services';
@Component({
  selector: 'app-merchant',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, ReactiveFormsModule, MatInputModule, MatDatepickerModule, MatNativeDateModule],
  templateUrl: './merchant.component.html',
  styleUrls: ['./merchant.component.css']
})
export class MerchantComponent implements OnInit {
  createAccountForm: (FormGroup & Merchant) | any;
  eyeIconClass: string = 'bi-eye';
  eyeIconClassConfirmPassword: string = 'bi-eye';
  showPassword = false;
  showConfirmPassword = false;

  constructor(
    public formbuilder: FormBuilder,
    public router: Router,
    public merchantService: MerchantService,
    public addressService: AddressService) { }

  ngOnInit(): void {
    const address: Address = {};
    this.createAccountForm = this.formbuilder.group({
      email: ['', [Validators.required, Validators.email]],
      name: ['', [Validators.required]],
      cpf: ['', [Validators.required]],
      cnpj: ['', [Validators.required]],
      phone: ['', [Validators.required]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
      zipcode: ['', [Validators.required]],
      street: ['', [Validators.required]],
      number: [''],
      complement: [''],
      neighborhood: ['', [Validators.required]],
      city: ['', [Validators.required]],
      state: ['', [Validators.required]],
      cardNumber: ['', [Validators.required]],
      cardValidate: ['', [Validators.required]],
      cardCVV: ['', [Validators.required]],
      address: [address],
    }) as (FormGroup & Merchant) | any;
  }

  onSaveClick = (): void => {
    let merchant: Merchant = {
      flatId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      name: this.createAccountForm.get("name").value,
      email: this.createAccountForm.get("email").value,
      password: this.createAccountForm.get("password").value,
      cpf: this.createAccountForm.get("cpf").value,
      cnpj: this.createAccountForm.get("cnpj").value,
      phone: this.createAccountForm.get("phone").value,
      address: this.createAccountForm.get("address").value,
      card: {
        number: this.createAccountForm.get("cardNumber").value,
        validate: this.createAccountForm.get("cardValidate").value,
        cvv: this.createAccountForm.get("cardCVV").value
      }
    };
    merchant.address.number = this.createAccountForm.get("number").value;
    merchant.address.complement = this.createAccountForm.get("complement").value;
    merchant.address.neighborhood = this.createAccountForm.get("neighborhood").value;
    merchant.address.city = this.createAccountForm.get("city").value;
    merchant.address.state = this.createAccountForm.get("state").value;

    this.merchantService.create(merchant)
    .subscribe({
      next: (response:any) => {
        if (response != null) {
          this.router.navigate(['/login']);
        }
        else {
          throw (response);
        }
      },
      error :(response : any) =>  {
        alert(response.error);
      },
      complete() {

      }
    });
  }

  buscarCEP() {
    const cep = this.createAccountForm.get('zipcode').value.replace('-', '');
    this.addressService.buscarCep(cep).subscribe((address: Address) => {
      if (address) {
        this.createAccountForm.patchValue({
          street: address.street,
          neighborhood: address.neighborhood,
          city: address.city,
          state: address.state,
          address: address
        });
      } else {
        alert('Endereço não encontrado!')
      }
    });
  }

  onTooglePassword = (): void => {
    this.showPassword = !this.showPassword;
    this.eyeIconClass = (this.eyeIconClass === 'bi-eye') ? 'bi-eye-slash' : 'bi-eye';
  }

  onToogleConfirmPassword = (): void => {
    this.showConfirmPassword = !this.showConfirmPassword;
    this.eyeIconClassConfirmPassword = (this.eyeIconClassConfirmPassword === 'bi-eye') ? 'bi-eye-slash' : 'bi-eye';
  }

  isPasswordValid = (): boolean => {
    let senha = this.createAccountForm.get('password').value;
    let confirmaSenha = this.createAccountForm.get('confirmPassword').value;
    return (senha !== confirmaSenha);
  }
}
