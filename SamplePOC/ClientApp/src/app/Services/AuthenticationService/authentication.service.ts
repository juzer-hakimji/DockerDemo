import { Injectable, Inject } from '@angular/core';
import { User } from '../../Models/User';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  public currentUserSubject: BehaviorSubject<User>;
  BaseURL: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this.BaseURL = baseUrl;
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(Email: string, password: string) {
    return this.http.post<User>(this.BaseURL + 'api/UserAuthentication/Login', { Email: Email, Password: password })
      .pipe(map(user => {
        if (user && user.Token) {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
        }
        return user;
      }));
  }

  UserRegistration(UserDetails: User) {
    return this.http.post<any>(this.BaseURL + 'api/UserAuthentication/Registration', UserDetails).pipe(map(user => {
      if (user && user.Token) {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
      }
      return user;
    }));
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}
