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
    // this.client.create4("1", this.job).subscribe((response) => {
    //   console.log(response);
    // });
  }

  selectExp(exp){
    this.job.experience = exp;
  }
}
