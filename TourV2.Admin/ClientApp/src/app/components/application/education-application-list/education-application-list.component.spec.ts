import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EducationApplicationListComponent } from './education-application-list.component';

describe('EducationApplicationListComponent', () => {
  let component: EducationApplicationListComponent;
  let fixture: ComponentFixture<EducationApplicationListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EducationApplicationListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EducationApplicationListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
