import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FavoritesBarComponent } from './favorites-bar.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('FavoritesBarComponent', () => {
  let component: FavoritesBarComponent;
  let fixture: ComponentFixture<FavoritesBarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FavoritesBarComponent],
      imports: [HttpClientTestingModule]
    });
    fixture = TestBed.createComponent(FavoritesBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
