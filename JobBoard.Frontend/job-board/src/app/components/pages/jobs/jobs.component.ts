import { Component, OnInit, Inject } from "@angular/core";
import { Client, JobLookupDto } from "src/app/api/api";
import { environment } from "../../../../environments/environment";
@Component({
  selector: "app-jobs",
  templateUrl: "./jobs.component.html",
  styleUrls: ["./jobs.component.scss"],
})
export class JobsComponent implements OnInit {
  public jobs : JobLookupDto[];
  constructor(public client: Client) {}

  ngOnInit(): void {
    this.client.getAll('1').subscribe(result => {
      this.jobs = result.jobs;
      console.log(this.jobs);
    });
  }
}
