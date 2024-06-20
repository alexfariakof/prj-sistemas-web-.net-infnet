import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MusicComponent } from './musics.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { SharedModule } from '../../components/shared.module';

describe('MusicsComponent', () => {
  let component: MusicComponent;
  let fixture: ComponentFixture<MusicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule, SharedModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MusicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
