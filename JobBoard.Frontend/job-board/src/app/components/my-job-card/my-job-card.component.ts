import { Component, Input, OnInit } from '@angular/core';
import { Job } from 'src/app/api/api';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-my-job-card',
  templateUrl: './my-job-card.component.html',
  styleUrls: ['./my-job-card.component.scss']
})

export class MyJobCardComponent implements OnInit {
  @Input() public job : Job;

  constructor(private router: Router) { }

  ngOnInit(): void { }

  openJobDetails() {
    this.router.navigate(['/jobs/:id', {id : this.job.id}]); 
    console.log(this.job)
  }

  update() {

  }

  delete() {
    
  }
}
 