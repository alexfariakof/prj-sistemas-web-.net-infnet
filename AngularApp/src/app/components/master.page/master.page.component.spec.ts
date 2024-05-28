import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MasterPageComponent } from './master.page.component';
import { MasterPageModule } from './master.page.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('MasterPageComponent', () => {
  let component: MasterPageComponent;
  let fixture: ComponentFixture<MasterPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MasterPageComponent],
      imports: [HttpClientTestingModule, MasterPageModule]
    });
    fixture = TestBed.createComponent(MasterPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
