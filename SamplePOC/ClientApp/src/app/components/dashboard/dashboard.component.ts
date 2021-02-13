import { Component, OnInit } from '@angular/core';
import { User } from '../../Models/User';
import { UserOperationsService } from '../../Services/UserOperationsService/user-operations.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public Users: User[];

  constructor(private UserOperationsService: UserOperationsService) {
    this.UserOperationsService.GetUserList()
      .subscribe(
        data => {
          this.Users = data;
        });
  }

  ngOnInit() {
  }

}
