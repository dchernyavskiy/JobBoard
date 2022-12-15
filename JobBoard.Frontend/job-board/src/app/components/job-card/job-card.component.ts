import { Component, Input, OnInit } from '@angular/core';
import { JobLookupDto, EmployeeVm, Client } from 'src/app/api/api';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-job-card',
  templateUrl: './job-card.component.html',
  styleUrls: ['./job-card.component.scss']
})

export class JobCardComponent implements OnInit {
  @Input() public job : JobLookupDto;
  public employee: EmployeeVm;

  constructor(private router: Router, private client: Client) { 
    
  }

  ngOnInit(): void { 
    
  }

  openJobDetails() {
    this.router.navigate(['/jobs/:id', {id : this.job.id}]); 
  }

  applyJob() {
    if(this.employee!=null){ //НЕ ЗАБЫТЬ ИЗМЕНИТЬ, Я НЕ ВИНОВАТ ЧТО ЛОКАЛ СТОРЕДЖ НЕ МЕНЯЕТ РОЛЬ ПОСЛЕ АВТОРИЗАЦИИ
      localStorage.setItem("role","Employee");
    }
    if(localStorage.getItem("role") == "Employer" || localStorage.getItem("role") == "SystemAdministrator"){
      window.alert("You have to be employee");
    }
    else if(localStorage.getItem("role") == null){
      window.alert("You have to sign in");
    }
    else{
      if(window.confirm("Do you really want to apply to this job?")){
        this.client.uCreate(this.employee.id,'1',{jobId:this.job.id}).subscribe(res =>{
        });
      }
    }
  }

  addtoFavourites() {
    
  }
}
 