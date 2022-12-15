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

  constructor(private router: Router, private client: Client) { 
    
  }

  ngOnInit(): void { 
  }

  openJobDetails() {
    this.router.navigate(['/jobs/:id', {id : this.job.id}]); 
  }

  applyJob() {
    if(localStorage.getItem("role") == "Employer" || localStorage.getItem("role") == "SystemAdministrator"){
      window.alert("You have to be employee");
    }
    else if(localStorage.getItem("role") == 'undefined'){
      window.alert("You have to sign in");
    }
    else{
      if(window.confirm("Do you really want to apply to this job?")){
        this.client.create('1',{jobId:this.job.id}).subscribe(res =>{
        });
      }
    }
  }

  setFavourite(){
    if(localStorage.getItem("role") == "Employer" || localStorage.getItem("role") == "SystemAdministrator"){
      window.alert("You have to be employee");
    }
    else if(localStorage.getItem("role") == null){
      window.alert("You have to sign in");
    }
    else{
      if(window.confirm("Do you really want to save this job?")){
        this.client.likeJob(this.job.id,'1').subscribe(res =>{
        });
      }
    }
  }
}
 