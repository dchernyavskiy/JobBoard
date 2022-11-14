import { Component, OnInit } from "@angular/core";
import { Client, GetJobsQuery, JobLookupDto, JobFilter, Pagging, JobSort } from "src/app/api/api";
import { environment } from "../../../environments/environment";
import { JobService } from "src/app/services/job.service";
import { JobsnewComponent } from "../jobsnew/jobsnew.component";

@Component({
  selector: "app-jobs",
  templateUrl: "./jobs.component.html",
  styleUrls: ["./jobs.component.scss"],

})

export class JobsComponent implements OnInit {
  public jobs : JobLookupDto[];
  public body : GetJobsQuery;
  

  constructor(public client: Client, public jobService : JobService) {
    this.body = {
filters:{
        keyWord : null,
        categoryIds : null,
        locationIds : null,
       salaryStart : 0,
       salaryEnd : 0,
       emloyerIds : null,
      experiences : null
      },
      pagging: {
        count: 12,
        page: 1
     },
      sort: {
        sortByName: false,
        sortBySalary: false,
        sortByExpirience: false,
        isAscending: true
     }
    }
 }
  

  ngOnInit(): void {


    this.client.getAllPOST('1', this.body).subscribe(result =>{
      this.jobs = result.jobs;  
      console.log(result);
});

     this.client.getAllGET('1').subscribe(result =>{
       console.log(result.categories);
     });
  }


}