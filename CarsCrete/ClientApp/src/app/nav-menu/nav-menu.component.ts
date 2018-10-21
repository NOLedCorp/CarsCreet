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
    
  }

  collapse() {
    this.isExpanded = false;
  }
  Exit(){
    this.userService.currentUser=null;
  }
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
