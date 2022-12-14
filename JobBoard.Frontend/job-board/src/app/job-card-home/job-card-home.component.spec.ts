import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobCardHomeComponent } from './job-card-home.component';

describe('JobCardHomeComponent', () => {
  let component: JobCardHomeComponent;
  let fixture: ComponentFixture<JobCardHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobCardHomeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobCardHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
