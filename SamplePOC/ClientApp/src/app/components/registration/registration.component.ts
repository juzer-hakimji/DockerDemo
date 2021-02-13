import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from '../../Services/AuthenticationService/authentication.service';
import { Router } from '@angular/router';
import { User } from '../../Models/User';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  RegistrationForm: FormGroup;


  constructor(private fb: FormBuilder, private UserAuthenticationService: AuthenticationService, private router: Router) { }

  ngOnInit() {
    this.RegistrationForm = this.fb.group({
      FirstName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50), Validators.pattern('^[a-zA-Z]+$')]],
      LastName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50), Validators.pattern('^[a-zA-Z]+$')]],
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.pattern('^(?=.*?[a-zA-Z])(?=.*?[0-9]).{8,}$')]],
      MobileNumber: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10), Validators.pattern('^[0-9]+$')]],
    });
  }

  onSubmit() {
    
    if (this.RegistrationForm.invalid) {
      return;
    }

    var formValue = this.RegistrationForm.value;
    this.UserAuthenticationService.UserRegistration(new User(formValue.FirstName, formValue.LastName, formValue.MobileNumber, formValue.Email, formValue.Password))
      .subscribe(
        data => {
          this.router.navigate([""]);
        },
        error => {
          alert(error);
        });
  }
}
