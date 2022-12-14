import { Component, OnInit } from '@angular/core';
import { JobVm, Client, EmployeeVm } from "src/app/api/api";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-job-details',
  templateUrl: './job-details.component.html',
  styleUrls: ['./job-details.component.scss']
})
export class JobDetailsComponent implements OnInit {
  public job : JobVm = {}
  public employee: EmployeeVm;
  constructor(private route: ActivatedRoute, private client: Client) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(res => {
      console.log();
      this.client.get3(res.get("id"), '1').subscribe(res => {
        this.job = res;
      });
    })
    this.client.get("1").subscribe((res) => {
      this.employee = res as EmployeeVm;
    });
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
}
