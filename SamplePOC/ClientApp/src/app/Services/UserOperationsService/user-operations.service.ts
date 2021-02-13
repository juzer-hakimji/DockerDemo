import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../Models/User';
import { Country } from '../../Models/Country';

@Injectable({
  providedIn: 'root'
})
export class UserOperationsService {

  BaseURL: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.BaseURL = baseUrl;
  }

  GetUserList() {
    return this.http.get<User[]>(this.BaseURL + 'api/UserOperations/UserList');
  }

  GetProfileData(UserId: number) {
    return this.http.get<User>(this.BaseURL + 'api/UserOperations/ProfileData' + '/' + UserId);
  }

  SaveUserDetails(User: User) {
    User.Gender = User.Gender == "" ? null : User.Gender;
    User.LanguageId = User.LanguageId == "" ? null : User.LanguageId;
    return this.http.post<any>(this.BaseURL + 'api/UserOperations/SaveUserDetails', User);
  }

  SaveCountry(CountryModel: Country) {
    return this.http.post<Country[]>(this.BaseURL + 'api/UserOperations/SaveCountry', CountryModel);
  }

  DeleteCountry(CountryId:number,UserId:number) {
    return this.http.get<Country[]>(this.BaseURL + 'api/UserOperations/DeleteCountry' + '/' + CountryId + '/' + UserId);
  }

  GetCountryList(UserId: number) {
    return this.http.get<Country[]>(this.BaseURL + 'api/UserOperations/GetCountries' + '/' + UserId);
  }
}
