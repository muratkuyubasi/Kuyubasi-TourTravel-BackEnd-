import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HajjApplicationDetailComponent } from './hajj-application-detail.component';

describe('HajjApplicationDetailComponent', () => {
  let component: HajjApplicationDetailComponent;
  let fixture: ComponentFixture<HajjApplicationDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HajjApplicationDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HajjApplicationDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
