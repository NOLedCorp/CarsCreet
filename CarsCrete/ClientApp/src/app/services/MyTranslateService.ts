import {TranslateService} from '@ngx-translate/core';

export class MyTranslateService {
    lang:string;
    constructor(private translate: TranslateService){
        this.translate.setDefaultLang('ru');
      
        this.changeLang('ru');
      }
  
    changeLang(lang:string){
        this.lang=lang;
        this.translate.use(lang);
        console.log(this.translate);
       
    }
  }