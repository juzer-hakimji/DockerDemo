import { Component, OnInit } from '@angular/core';
import { Country } from '../../Models/Country';
import { UserOperationsService } from '../../Services/UserOperationsService/user-operations.service';
import { AuthenticationService } from '../../Services/AuthenticationService/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-country-master',
  templateUrl: './country-master.component.html',
  styleUrls: ['./country-master.component.css']
})
export class CountryMasterComponent implements OnInit {

  CountryList: Country[];
  CountryModel: Country;

  constructor(private UserOperationsService: UserOperationsService, private authenticationService: AuthenticationService,
    private router: Router) {
  }

  ngOnInit() {
    this.CountryModel = new Country("", "", null, null);
    this.UserOperationsService.GetCountryList(this.authenticationService.currentUserValue.UserId)
      .subscribe(
        data => {
          this.CountryList = data;
        },
        error => {
          alert(error);
        });
  }

  onSubmit() {
    this.CountryModel.UserId = this.authenticationService.currentUserValue.UserId;
    this.UserOperationsService.SaveCountry(this.CountryModel)
      .subscribe(
        data => {
          this.CountryList = data;
        },
        error => {
          alert(error);
        });
  }

  CountryEdit(CountryId: number) {
    this.CountryModel.CountryId = this.CountryList.find(x => x.CountryId == CountryId).CountryId;
    this.CountryModel.CountryName = this.CountryList.find(x => x.CountryId == CountryId).CountryName;
    this.CountryModel.CountryCode = this.CountryList.find(x => x.CountryId == CountryId).CountryCode;
  }

  CountryDelete(CountryId: number) {
    this.UserOperationsService.DeleteCountry(CountryId, this.authenticationService.currentUserValue.UserId)
      .subscribe(
        data => {
          this.CountryList = data;
        },
        error => {
          alert(error);
        });
  }
}
