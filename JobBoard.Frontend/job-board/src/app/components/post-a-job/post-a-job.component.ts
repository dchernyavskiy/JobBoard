import { Component, OnInit } from "@angular/core";
import {
  AppliedJobLookupDto,
  Client,
  CreateJobCommandDto,
} from "src/app/api/api";

@Component({
  selector: "app-post-a-job",
  templateUrl: "./post-a-job.component.html",
  styleUrls: ["./post-a-job.component.scss"],
})
export class PostAJobComponent implements OnInit {
  public job: CreateJobCommandDto = {};

  constructor(public client: Client) {}

  ngOnInit(): void {
  }
  
  createJob(){
    this.job.employment = "Full time";
    this.job.categoryId = "6E936B2E-DFF7-4A97-846A-44947B53F91A";
    this.job.location = "2262D9B0-24C2-4F61-AFFB-3935882D50A7";
    console.log(this.job);
    this.client.create4("1", this.job).subscribe((response) => {
       console.log(response);
    });
  }

  selectExp(exp){
    this.job.experience = exp;
  }
}
