import { Component, OnInit } from '@angular/core';
import { Client, EmployerLookupDto } from 'src/app/api/api';

@Component({
  selector: 'app-employers',
  templateUrl: './employers.component.html',
  styleUrls: ['./employers.component.scss']
})
export class EmployersComponent implements OnInit {
  public employers: EmployerLookupDto[] = [];
  public page: number = 1;
  public count: number = 12;
  public pageCount: number;
  public keyWord: string = '';
  constructor(public client: Client) { }

  ngOnInit(): void {
    this.getEmployers();
  }

  getEmployers(){
    this.client.getAllGET2('1').subscribe(res =>{
      this.employers = (res.employers as EmployerLookupDto[])
      
      .filter((u, i) => i >= (this.page-1)*this.count &&
       i <= ((this.page-1)*this.count) + this.count);

       this.pageCount  = res.employers.length;
    });
    
  }


}
