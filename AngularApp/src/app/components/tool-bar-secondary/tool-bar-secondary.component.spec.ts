import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ToolBarSecondaryComponent } from '..';
import ToolBarSecondaryModule from './tool-bar-secondary.module';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('ToolBarSecondaryComponent', () => {
  let component: ToolBarSecondaryComponent;
  let fixture: ComponentFixture<ToolBarSecondaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
    declarations: [ToolBarSecondaryComponent],
    imports: [ToolBarSecondaryModule],
    providers: [provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    fixture = TestBed.createComponent(ToolBarSecondaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
