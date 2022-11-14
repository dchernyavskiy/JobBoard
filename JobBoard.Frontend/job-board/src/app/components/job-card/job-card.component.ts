import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-job-card',
  templateUrl: './job-card.component.html',
  styleUrls: ['./job-card.component.scss']
})
export class JobCardComponent implements OnInit {
  @Input() public imgPath : string;
  @Input() public jobName : string;
  @Input() public location : string;
  @Input() public description : string;
  @Input() public category : string;
  @Input() public employment : string;

  constructor() { }

  ngOnInit(): void {
  }

}
