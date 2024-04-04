import { ComponentFixture, TestBed } from '@angular/core/testing';
import ToolBarSecondaryComponent from './tool-bar-secondary.module';

describe('ToolBarSecondaryComponent', () => {
  let component: ToolBarSecondaryComponent;
  let fixture: ComponentFixture<ToolBarSecondaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ToolBarSecondaryComponent]
    });
    fixture = TestBed.createComponent(ToolBarSecondaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
