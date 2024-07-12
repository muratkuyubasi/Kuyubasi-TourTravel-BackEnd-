import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PeriodsManageComponent } from './periods-manage.component';

describe('PeriodsManageComponent', () => {
  let component: PeriodsManageComponent;
  let fixture: ComponentFixture<PeriodsManageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PeriodsManageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PeriodsManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
