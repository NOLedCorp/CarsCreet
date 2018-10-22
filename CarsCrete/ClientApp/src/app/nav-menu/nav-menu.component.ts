import { Component, Input } from '@angular/core';
import {MyTranslateService} from '../services/MyTranslateService';
import {UserService} from '../services/UserService';
import { AlertService } from '../services/AlertService';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  @Input() service: MyTranslateService;
 
  alertService:AlertService = new AlertService();
  isExpanded = false;
  constructor(public userService:UserService){
    if(localStorage.getItem("currentUser")){
      this.userService.currentUser=JSON.parse(localStorage.getItem("currentUser"));
    }
  }

  collapse() {
    this.isExpanded = false;
  }
  Exit(){
    this.userService.currentUser=null;
    localStorage.removeItem("currentUser");
    location.reload();
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  changePage(page:string){
    
  }
}
