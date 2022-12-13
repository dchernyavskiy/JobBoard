import { Component, Input, OnInit } from '@angular/core';
import { JobLookupDto } from 'src/app/api/api';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-job-card',
  templateUrl: './job-card.component.html',
  styleUrls: ['./job-card.component.scss']
})

export class JobCardComponent implements OnInit {
  @Input() public job : JobLookupDto;

  constructor(private router: Router) { }

  ngOnInit(): void { }

  openJobDetails() {
    this.router.navigate(['/jobs/:id', {id : this.job.id}]); 
    console.log(this.job)
  }

  applyJob() {

  }

  addtoFavourites() {
    
  }
}
 