import { Component, OnInit } from '@angular/core';
import { Client, EmployeeVm } from 'src/app/api/api';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})

export class DashboardComponent implements OnInit {
  public employee: EmployeeVm = {};

  constructor(public client: Client) { }

  ngOnInit(): void {
    this.client.get('1').subscribe(res => {
      this.employee = res as EmployeeVm;
    });
  }

}
