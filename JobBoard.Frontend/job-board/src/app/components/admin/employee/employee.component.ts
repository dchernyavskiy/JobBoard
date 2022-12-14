import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Client, Employee } from 'src/app/api/api';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {
  public count = 1;
  public employees: Employee[];

  constructor(private client: Client, public oidcSecurityService: OidcSecurityService) { }

  ngOnInit(): void {
    this.client.getEmployees('1').subscribe(res =>{
      this.employees = res.employees as Employee[];
    });
  }

  ban(employee: Employee){
    employee.isBan = !employee.isBan;
    this.client.banEmployee(employee.id, '1').subscribe(res =>{
      console.log('succesfuly banned');
    });
  }

  exit() {
    this.oidcSecurityService.logoff();
  }
}
