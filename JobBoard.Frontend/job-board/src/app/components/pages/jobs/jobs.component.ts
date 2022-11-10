import { Component, OnInit, Inject } from "@angular/core";
import { Client, GetJobsQuery, JobLookupDto, JobFilter, Pagging, JobSort } from "src/app/api/api";
import { environment } from "../../../../environments/environment";
@Component({
  selector: "app-jobs",
  templateUrl: "./jobs.component.html",
  styleUrls: ["./jobs.component.scss"],
})
export class JobsComponent implements OnInit {
  public jobs : JobLookupDto[];
  public body : GetJobsQuery;

  constructor(public client: Client) {
    this.body = {
      filters:{
        keyWord : null,
        categoryIds : null,
        locationIds : null,
        salaryStart : null,
        salaryEnd : null,
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
    // this.client.getAll('1', this.body).subscribe(result => {
    //   this.jobs = result.jobs;
    //   console.log(this.jobs);
    // });
    this.client.create3("1", {
      name: "Detroit"
    }).subscribe(result => {
      console.log(result);
    });
  }
}
