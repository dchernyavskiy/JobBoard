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
  public currentPage: number = 1;

  constructor(public client: Client) { }

  ngOnInit(): void {
    this.getEmployers();
  }

  getEmployers(){
    this.client.getAllGET2('1').subscribe(res =>{
      this.employers = (res.employers as EmployerLookupDto[])
      
      .filter((u, i) => i >= (this.currentPage-1)*this.count &&
       i < ((this.currentPage-1)*this.count) + this.count);
       this.pageCount  = Math.ceil(res.employers.length / this.count);
    });
  }

  setPage(page: number) {
    this.currentPage = page;
    this.getEmployers();
  }

  range(count: number): number[] {
    let arr = [];
    for (let i = 1; i <= count; i++) {
      arr.push(i);
    }
    return arr;
  }


}
