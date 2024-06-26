import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CustomInterceptor } from './interceptors/http.interceptor.service';
import { AlbumModule } from './pages/album/album.module';
import { BandModule } from './pages/band/band.module';
import { MusicModule } from './pages/musics/musics.module';
import { MyplaylistModule } from './pages/myplaylist/myplaylist.module';
import { PlaylistModule } from './pages/playlist/playlist.module';
import { MAT_DATE_LOCALE, DateAdapter, MAT_DATE_FORMATS } from '@angular/material/core';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS, MAT_MOMENT_DATE_FORMATS } from '@angular/material-moment-adapter';

@NgModule({ declarations: [AppComponent],
    bootstrap: [AppComponent], imports: [BrowserModule, BrowserAnimationsModule, AppRoutingModule,
        MyplaylistModule, PlaylistModule, AlbumModule, BandModule, MusicModule], providers: [
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, },
        { provide: MAT_DATE_LOCALE, useValue: 'pt-br' },
        {
            provide: DateAdapter,
            useClass: MomentDateAdapter,
            deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS],
        },
        { provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS },
        provideHttpClient(withInterceptorsFromDi()),
    ] })

export class AppModule { }
