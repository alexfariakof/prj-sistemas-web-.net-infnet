import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFavoritesComponent } from './add-favorites.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AddFavoritesComponent', () => {
  let component: AddFavoritesComponent;
  let fixture: ComponentFixture<AddFavoritesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddFavoritesComponent],
      imports: [HttpClientTestingModule]
    });
    fixture = TestBed.createComponent(AddFavoritesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
