import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LayoutComponent } from './components/layout/layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CountryMasterComponent } from './components/country-master/country-master.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { RedirectGuard } from './Helpers/Redirect.guard';
import { AuthGuard } from './Helpers/auth.guard';

const routes: Routes = [
  { path: '', component: LoginComponent, pathMatch: 'full', canActivate: [RedirectGuard] },
  { path: 'Login', component: LoginComponent },
  { path: 'Registration', component: RegistrationComponent },
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: 'Dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
      { path: 'CountryMaster', component: CountryMasterComponent, canActivate: [AuthGuard] },
      { path: 'UserProfile', component: UserProfileComponent, canActivate: [AuthGuard] }
    ]
  },

  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
