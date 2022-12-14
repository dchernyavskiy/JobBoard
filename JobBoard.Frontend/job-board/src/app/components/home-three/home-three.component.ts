import { Component, OnInit } from "@angular/core";
import { Client, JobLookupDto, GetJobsQuery } from 'src/app/api/api';

@Component({
  selector: "app-home-three",
  templateUrl: "./home-three.component.html",
  styleUrls: ["./home-three.component.scss"],
})
export class HomeThreeComponent implements OnInit {
  public jobs: JobLookupDto[] = [];
  public page: number = 1;
  public count: number = 12;
  public pageCount: number;
  public resultCount: number = 0;
  public body: GetJobsQuery;


  constructor(public client: Client) {
  
  }

  ngOnInit(): void {
    this.body = {
      filters: {
        keyWord: "",
        categoryIds: null,
        locationIds: null,
        salaryStart: 0,
        salaryEnd: 0,
        emloyerIds: null,
        experiences: null,
      },
      pagging: {
        count: 6,
        page: 1,
      },
      sort: {
        sortByName: false,
        sortBySalary: false,
        sortByExpirience: false,
        isAscending: true,
      },
    };
    this.getJobs();
  }

  getJobs() {
    this.client.getAllPOST("1", this.body).subscribe((result) => {
      this.jobs = result.jobs;
      this.resultCount = result.resultCount;
      this.pageCount = result.pageCount;
    });
  }
}
