import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {TranslateModule, TranslateLoader} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';

import { HttpClientModule, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { CarsComponent } from './cars/cars.component';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { UserFormComponent } from './user-form/user-form.component';
import { UserService } from './services/UserService';
import { AlertComponent } from './alert/alert.component';
import { CarsService } from './services/CarsService';
export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, '../assets/i18n/', '.json');
}
export function HttpLoaderFactory1(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    CarsComponent,
    UserFormComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    AngularFontAwesomeModule,
    TranslateModule.forRoot({
      loader:{
        provide: TranslateLoader,
        // можно указать свой путь к папке i18n где находятся файлы с переводом
        useFactory: (HttpLoaderFactory),
        deps: [HttpClient]
      }
    }),
    
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'cars', component: CarsComponent },
    ])
  ],
  providers: [UserService, CarsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
