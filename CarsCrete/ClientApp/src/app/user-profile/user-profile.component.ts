import { Component, OnInit } from '@angular/core';
import {User, UserService, Book} from '../services/UserService';
import {Router} from '@angular/router';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'

const URL = '/cars/upload-user-photo';
@Component({
  selector: 'user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  
  changes:boolean[]=[false,false,false];
  constructor(private http: HttpClient, public userService:UserService, private router: Router) { }

  ngOnInit() {
    
    if(localStorage.getItem("currentUser")){
      this.userService.currentUser=JSON.parse(localStorage.getItem("currentUser"));
      this.userService.GetUserById(this.userService.currentUser.Id).subscribe(data => {
        data.Topics.forEach(x => {
          x.ModifyDate= new Date(x.ModifyDate);
        })
        this.userService.currentUser=data;
        console.log(this.userService.currentUser);
        localStorage.setItem('currentUser',JSON.stringify(data));
       
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
  changeInfo(item:number,type:string, value:string){
    if(value != ''){
      
      this.userService.ChangeInfo(type, value, this.userService.currentUser.Id).subscribe(data => {
        if(data){
          this.userService.currentUser[type]=value;
          console.log(data);
        }
      })
    }
    
    this.changes[item]=false;
  }
  upload(files) {
    console.log(files);
    const formData = new FormData();
    formData.append(files[0].Name, files[0]);
    // this.userService.UploadPhoto(formData).subscribe(data => {
    //   console.log(true);
    // })
    const req = new HttpRequest('POST', `cars/upload-user-photo`, formData);

    this.http.request(req).subscribe(event => {
        
        if (event instanceof HttpResponse)
            console.log('Files uploaded!');
    });

      
  }
  showChangeInfo(item:number, show:boolean){

    for(let i =0; i<this.changes.length;i++){
      if(i==item){
        this.changes[i]=show;
      }
      else{
        this.changes[i]=false;
      }
      
    }
  }

}
