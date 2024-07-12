import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UmrahApplicationListComponent } from './umrah-application-list.component';

describe('UmrahApplicationListComponent', () => {
  let component: UmrahApplicationListComponent;
  let fixture: ComponentFixture<UmrahApplicationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UmrahApplicationListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UmrahApplicationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
