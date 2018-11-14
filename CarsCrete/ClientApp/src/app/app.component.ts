import { Component, OnInit } from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import { Router, NavigationEnd } from '@angular/router';
import {MyTranslateService} from './services/MyTranslateService';
import { IMPLICIT_REFERENCE } from '@angular/compiler/src/render3/view/util';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'app';
  showMes:boolean = true;
  service:MyTranslateService ;
  constructor(private router:Router, private translate: TranslateService){
    this.service = new MyTranslateService(translate);
    
  }
  ngOnInit(){
    window.scrollTo(0, 0);
    this.router.events.subscribe((evt) => {
      
      if (!(evt instanceof NavigationEnd)) {
          return;
      }
      if(evt.url=='/user'){
        this.showMes=false;
      }
      else{
        this.showMes=true;
      }
      window.scrollTo(0, 0)
     });
  }
  // changeLang(lang:string){
  //   console.log(lang);
  //   this.translate.use(lang);
  // }
}
