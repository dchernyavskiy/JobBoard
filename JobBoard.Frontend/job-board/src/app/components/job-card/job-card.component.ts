import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-job-card',
  templateUrl: './job-card.component.html',
  styleUrls: ['./job-card.component.scss']
})
export class JobCardComponent implements OnInit {
  public imgPath : string;
  public jobName : string;
  public location : string;
  public description : string;
  public category : string;
  public employment : string;

  constructor() { }

  ngOnInit(): void {
  }

}
