import { Component, Input, OnInit } from '@angular/core';
import { Client, Job } from 'src/app/api/api';
import { Router } from '@angular/router'; 
import { JobCardComponent } from '../job-card/job-card.component';

@Component({
  selector: 'app-my-job-card',
  templateUrl: './my-job-card.component.html',
  styleUrls: ['./my-job-card.component.scss']
})

export class MyJobCardComponent implements OnInit {
  @Input() public job : Job;

  constructor(private router: Router, private client: Client) { }

  ngOnInit(): void { }

  openJobDetails() {
    this.router.navigate(['/jobs/:id', {id : this.job.id}]); 
    console.log(this.job)
  }

  delete() {
    this.client.delete3(this.job.id, "1").subscribe(res => {
      console.log(res);
    });
    location.reload();
  }
}
 