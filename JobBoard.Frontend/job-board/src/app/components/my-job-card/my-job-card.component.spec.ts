import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyJobCardComponent } from './my-job-card.component';

describe('MyJobCardComponent', () => {
  let component: MyJobCardComponent;
  let fixture: ComponentFixture<MyJobCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyJobCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyJobCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
