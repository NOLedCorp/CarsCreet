import { Component, OnInit } from '@angular/core';
import {User, UserService, Book, Sale} from '../services/UserService';

import {Router} from '@angular/router';
import { HttpClient, HttpRequest, HttpEventType, HttpResponse } from '@angular/common/http'
import { NewCar, Car, CarsService, ReportCar } from '../services/CarsService';

const URL = '/cars/upload-user-photo';
@Component({
  selector: 'user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {
  showBooks:boolean = true;
  showAddCar:boolean = true;
  showAddSale:boolean = true;
  cars:ReportCar[] = [];
  saleErrors:any={DateStrart:true, DateFinish:true};
  newSale:Sale = new Sale();
  newCar:NewCar =new NewCar();
  changes:boolean[]=[false,false,false];
  constructor(public carsService:CarsService, private http: HttpClient, public userService:UserService, private router: Router) { }

  ngOnInit() {
    
    if(localStorage.getItem("currentUser")){
      this.userService.currentUser=JSON.parse(localStorage.getItem("currentUser"));
      this.userService.GetUserById(this.userService.currentUser.Id).subscribe(data => {
        data.Topics.forEach(x => {
          x.ModifyDate= new Date(x.ModifyDate);
        })
        this.userService.currentUser=data;
        localStorage.setItem('currentUser',JSON.stringify(data));
       
      })
      if(this.userService.currentUser.IsAdmin){
        this.carsService.GetReportCars().subscribe( data => {
          this.cars=data;
      
        })
      }
    }
    else{
      this.router.navigate(['/allcars']);
    }
    
    
  }
  show(prop:string){
    this[prop] = !this[prop];
  }
  getSalePrice(){
    if(this.newSale.CarId!=0 && this.newSale.Discount){
      let carPrice = this.cars.find(x => x.Id == this.newSale.CarId).Price;
      let res = (carPrice*(1-this.newSale.Discount/100));
      return res;
    }
    return 0;
  }
  getStatus(book:Book){
    book.DateFinish = new Date(book.DateFinish);
    if(book.DateFinish.getTime()<new Date().getTime()){
      return "Завершено"
    }else{
      if(new Date(book.DateStart).getTime()<=new Date().getTime() && book.DateFinish.getTime()>=new Date().getTime()){
        return "Активно"
      }
      else{
        return "Ожидание"
      }
      
    }
  }
  changeInfo(item:number,type:string, value:string){
    if(value != ''){
      
      this.userService.ChangeInfo(type, value, this.userService.currentUser.Id).subscribe(data => {
        if(data){
          this.userService.currentUser[type]=value;
          localStorage.setItem('currentUser', JSON.stringify(this.userService.currentUser));
        }
      })
    }
    
    this.changes[item]=false;
  }
  upload(files) {
    const formData = new FormData();
    formData.append(files[0].Name, files[0]);
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
