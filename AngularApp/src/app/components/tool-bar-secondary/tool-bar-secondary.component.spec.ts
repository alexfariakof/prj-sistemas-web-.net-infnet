import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ToolBarSecondaryComponent } from '..';
import ToolBarSecondaryModule from './tool-bar-secondary.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('ToolBarSecondaryComponent', () => {
  let component: ToolBarSecondaryComponent;
  let fixture: ComponentFixture<ToolBarSecondaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ToolBarSecondaryComponent],
      imports: [HttpClientTestingModule, ToolBarSecondaryModule]
    });
    fixture = TestBed.createComponent(ToolBarSecondaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
