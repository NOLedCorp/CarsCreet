import { Component } from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import {MyTranslateService} from './services/MyTranslateService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  service:MyTranslateService ;
  constructor(private translate: TranslateService){
    this.service = new MyTranslateService(translate);
    
  }
  // changeLang(lang:string){
  //   console.log(lang);
  //   this.translate.use(lang);
  // }
}
