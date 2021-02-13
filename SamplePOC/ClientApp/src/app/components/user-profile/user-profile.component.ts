import { Component, OnInit } from '@angular/core';
import { User } from '../../Models/User';
import { UserOperationsService } from '../../Services/UserOperationsService/user-operations.service';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../Services/AuthenticationService/authentication.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  User: User;
  Genders = { 0: 'Other', 1: 'Male', 2: 'Female' };
  CountryId: number;

  constructor(private UserOperationsService: UserOperationsService, private router: Router,
    private authenticationService: AuthenticationService) {
    this.User = new User("","","","","",null,null,null,null,null,null,null);
    this.GetProfileData();
  }

  ngOnInit() {
  }

  GetProfileData() {
    this.UserOperationsService.GetProfileData(this.authenticationService.currentUserValue.UserId)
      .subscribe(
        data => {
          this.User = data;
          if (this.User.CountryId) {
            this.CountryId = this.User.CountryId;
          }
        });
  }

  onSubmit() {
    this.UserOperationsService.SaveUserDetails(this.User)
      .subscribe(
        data => {
          alert(data.message);
        },
        error => {
          alert(error);
        });
  }

}
