import { Component, Input, OnInit } from '@angular/core';
import { JobLookupDto } from 'src/app/api/api';

@Component({
  selector: 'app-job-card',
  templateUrl: './job-card.component.html',
  styleUrls: ['./job-card.component.scss']
})

export class JobCardComponent implements OnInit {
  @Input() public job : JobLookupDto;
  // @Input() public imgPath : string;
  // @Input() public jobName : string;
  // @Input() public location : string;
  // @Input() public description : string;
  // @Input() public category : string;
  // @Input() public employment : string;

  constructor() { }

  ngOnInit(): void {
    console.log(this.job);
  }

}
