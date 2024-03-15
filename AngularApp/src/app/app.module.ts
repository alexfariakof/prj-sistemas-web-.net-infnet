import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { MatToolbarModule} from '@angular/material/toolbar';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CustomInterceptor } from './interceptors/http.interceptor.service';
import { AlbumModule } from './pages/album/album.module';
import { BandModule } from './pages/band/band.module';
import { HomeModule } from './pages/home/home.module';
import { MusicModule } from './pages/musics/musics.module';
import { MyplaylistModule } from './pages/myplaylist/myplaylist.module';

@NgModule({
  declarations: [AppComponent ],
  imports: [ BrowserModule, BrowserAnimationsModule, AppRoutingModule, HttpClientModule, CommonModule, ReactiveFormsModule,
    MatToolbarModule, MatFormFieldModule, MatInputModule, MatDatepickerModule,
  AlbumModule, BandModule, HomeModule, MusicModule, MyplaylistModule  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, },
  ],
  bootstrap: [AppComponent]
})

export class AppModule { }
