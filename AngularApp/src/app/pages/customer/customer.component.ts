import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import * as dayjs from 'dayjs';
import { Address, Customer } from 'src/app/model';
import { AddressService, CustomerService } from 'src/app/services';

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, ReactiveFormsModule, MatInputModule, MatDatepickerModule, MatNativeDateModule],
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  createAccountForm: (FormGroup & Customer) | any;
  eyeIconClass: string = 'bi-eye';
  eyeIconClassConfirmPassword: string = 'bi-eye';
  showPassword = false;
  showConfirmPassword = false;

  constructor(
    public formbuilder: FormBuilder,
    public router: Router,
    public customerService: CustomerService,
    public addressService: AddressService) { }

  ngOnInit(): void {
    const address: Address={};
    this.createAccountForm = this.formbuilder.group({
      email: ['', [Validators.required, Validators.email]],
      name: ['', [Validators.required]],
      cpf: ['', [Validators.required]],
      birth: [dayjs().format('YYYY-MM-DD'), Validators.required],
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
    }) as (FormGroup & Customer) | any;
  }

  onSaveClick = (): void => {
    let customer: Customer = {
      flatId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      name: this.createAccountForm.get("name").value,
      email: this.createAccountForm.get("email").value,
      password: this.createAccountForm.get("password").value,
      cpf: this.createAccountForm.get("cpf").value,
      birth: this.createAccountForm.get("birth").value,
      phone: this.createAccountForm.get("phone").value,
      address: this.createAccountForm.get("address").value,
      card: {
        number: this.createAccountForm.get("cardNumber").value,
        validate: this.createAccountForm.get("cardValidate").value,
        cvv: this.createAccountForm.get("cardCVV").value
      }
    };
    customer.address.number = this.createAccountForm.get("number").value;
    customer.address.complement = this.createAccountForm.get("complement").value;
    customer.address.neighborhood = this.createAccountForm.get("neighborhood").value;
    customer.address.city = this.createAccountForm.get("city").value;
    customer.address.state = this.createAccountForm.get("state").value;

    this.customerService.create(customer)
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
