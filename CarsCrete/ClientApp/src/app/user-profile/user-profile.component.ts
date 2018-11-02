import { Component, OnInit } from '@angular/core';
import {User, UserService, Book} from '../services/UserService';
import {Router} from '@angular/router';

@Component({
  selector: 'user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  constructor(public userService:UserService, private router: Router) { }

  ngOnInit() {
    
    if(localStorage.getItem("currentUser")){
      this.userService.currentUser=JSON.parse(localStorage.getItem("currentUser"));
      this.userService.GetUserById(this.userService.currentUser.Id).subscribe(data => {
        this.userService.currentUser=data;
        localStorage.setItem('currentUser',JSON.stringify(data));
        console.log(data);
      })
    }
    else{
      this.router.navigate(['/allcars']);
    }
    
  }
  getStatus(book:Book){
    if(new Date(book.DateFinish).getTime()<new Date().getTime()){
      return "Завершено"
    }else{
      return "Активно"
    }
  }

}
