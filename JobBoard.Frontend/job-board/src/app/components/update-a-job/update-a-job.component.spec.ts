import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateAJobComponent } from './update-a-job.component';

describe('UpdateAJobComponent', () => {
  let component: UpdateAJobComponent;
  let fixture: ComponentFixture<UpdateAJobComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateAJobComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateAJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
