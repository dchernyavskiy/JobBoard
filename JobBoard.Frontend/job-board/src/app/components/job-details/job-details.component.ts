import { Component, OnInit } from '@angular/core';
import { JobVm, Client } from "src/app/api/api";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-job-details',
  templateUrl: './job-details.component.html',
  styleUrls: ['./job-details.component.scss']
})
export class JobDetailsComponent implements OnInit {
  public job : JobVm = {}
  constructor(private route: ActivatedRoute, private client: Client) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(res => {
      console.log();
      this.client.get3(res.get("id"), '1').subscribe(res => {
        this.job = res;
      });
    })
  }
}