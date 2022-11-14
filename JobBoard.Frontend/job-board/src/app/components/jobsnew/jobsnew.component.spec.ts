import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JobsnewComponent } from './jobsnew.component';

describe('JobsnewComponent', () => {
  let component: JobsnewComponent;
  let fixture: ComponentFixture<JobsnewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JobsnewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JobsnewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
