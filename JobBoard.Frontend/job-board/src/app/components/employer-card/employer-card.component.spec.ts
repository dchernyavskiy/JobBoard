import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployerCardComponent } from './employer-card.component';

describe('EmployerCardComponent', () => {
  let component: EmployerCardComponent;
  let fixture: ComponentFixture<EmployerCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployerCardComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployerCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
