import { Component, OnInit } from "@angular/core";
import {
  AppliedJobLookupDto,
  Client,
  UpdateJobCommandDto,
} from "src/app/api/api";

@Component({
  selector: "app-update-a-job",
  templateUrl: "./update-a-job.component.html",
  styleUrls: ["./update-a-job.component.scss"],
})
export class UpdateAJobComponent implements OnInit {
  public job: UpdateJobCommandDto = {};

  constructor(public client: Client) {}

  ngOnInit(): void {
  }
  
  createJob(){
    console.log(this.job);
    this.client.create4("1", this.job).subscribe((response) => {
       console.log(response);
    });
  }

  selectExp(exp){
    this.job.experience = exp;
  }
}
