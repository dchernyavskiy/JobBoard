import { Component, OnInit, Input } from '@angular/core';
import { JobLookupDto } from 'src/app/api/api';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-job-card-home',
  templateUrl: './job-card-home.component.html',
  styleUrls: ['./job-card-home.component.scss']
})
export class JobCardHomeComponent implements OnInit {
  @Input() public job : JobLookupDto;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  openJobDetails() {
    this.router.navigate(['/jobs/:id', {id : this.job.id}]); 
    console.log(this.job)
  }

}
