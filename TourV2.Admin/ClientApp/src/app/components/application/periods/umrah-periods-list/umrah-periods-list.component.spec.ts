import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UmrahPeriodsListComponent } from './umrah-periods-list.component';

describe('UmrahPeriodsListComponent', () => {
  let component: UmrahPeriodsListComponent;
  let fixture: ComponentFixture<UmrahPeriodsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UmrahPeriodsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UmrahPeriodsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
