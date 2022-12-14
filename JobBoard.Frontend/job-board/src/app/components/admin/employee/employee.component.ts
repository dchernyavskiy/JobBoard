import { Component, OnInit } from '@angular/core';
import { Client, Employee } from 'src/app/api/api';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {
  public employees: Employee[];

  constructor(private client: Client) { }

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

}
