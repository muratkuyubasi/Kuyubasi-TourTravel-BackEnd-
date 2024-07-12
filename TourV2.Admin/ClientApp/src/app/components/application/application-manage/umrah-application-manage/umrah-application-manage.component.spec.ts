import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UmrahApplicationManageComponent } from './umrah-application-manage.component';

describe('UmrahApplicationManageComponent', () => {
  let component: UmrahApplicationManageComponent;
  let fixture: ComponentFixture<UmrahApplicationManageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UmrahApplicationManageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UmrahApplicationManageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
