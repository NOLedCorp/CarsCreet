import { Component, OnInit, HostListener  } from '@angular/core';
import {TranslateService} from '@ngx-translate/core';
import { Router, NavigationEnd } from '@angular/router';
import {MyTranslateService} from './services/MyTranslateService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'app';
  showMes:boolean = true;
  service:MyTranslateService ;
  showButtonUp:boolean = false;
  constructor(private router:Router, private translate: TranslateService){
    this.service = new MyTranslateService(translate);
    
  }
  @HostListener('document:scroll', [])
    onScroll(): void {
         if(window.pageYOffset>500){
           this.showButtonUp = true;
         }
         else{
          this.showButtonUp = false;
         }
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
  scroll(){
    window.scrollTo(0, 0);
  }
}
