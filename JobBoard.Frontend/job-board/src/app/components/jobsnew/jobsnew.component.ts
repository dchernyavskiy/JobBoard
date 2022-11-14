import { Component, OnInit } from '@angular/core';
import { JobService } from "src/app/services/job.service";
import { Client, GetJobsQuery, JobLookupDto, JobFilter, Pagging, JobSort } from "src/app/api/api";
import { JobCardComponent } from '../job-card/job-card.component';

@Component({
  selector: 'app-jobsnew',
  templateUrl: './jobsnew.component.html',
  styleUrls: ['./jobsnew.component.scss']
})

export class JobsnewComponent implements OnInit {
  items = ["1", "2", "3", "4"]
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

    // this.client.getAllGET('1').subscribe(result =>{
    //   console.log(result.categories);
    // });
  }


}
