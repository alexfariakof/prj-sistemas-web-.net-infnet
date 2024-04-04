import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MasterPageComponent } from './master.page.component';

describe('MasterPageComponent', () => {
  let component: MasterPageComponent;
  let fixture: ComponentFixture<MasterPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MasterPageComponent]
    });
    fixture = TestBed.createComponent(MasterPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
