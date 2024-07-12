import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UmrahApplicationDetailComponent } from './umrah-application-detail.component';

describe('UmrahApplicationDetailComponent', () => {
  let component: UmrahApplicationDetailComponent;
  let fixture: ComponentFixture<UmrahApplicationDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UmrahApplicationDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UmrahApplicationDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
