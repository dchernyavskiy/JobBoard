import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import {
  Client,
  EmployerVm,
} from "src/app/api/api";
@Component({
  selector: 'app-employers-details',
  templateUrl: './employers-details.component.html',
  styleUrls: ['./employers-details.component.scss']
})
export class EmployersDetailsComponent implements OnInit {
  public employer: EmployerVm;
  constructor(private route: ActivatedRoute, private client: Client) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params =>{
      this.client.get3(params.id,'1').subscribe(res =>{
        this.employer = res;
    (this.employer);
      })
    })
  }

}
