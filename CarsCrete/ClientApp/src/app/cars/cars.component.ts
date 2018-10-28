import { Component, Inject, OnInit } from '@angular/core';
import { AlertService } from '../services/AlertService';

import {User} from '../services/UserService';
import {CarsService, Car, Filter, Book} from '../services/CarsService';

@Component({
  selector: 'cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']

})
export class CarsComponent {
  load:boolean=true;
  public user:User;

  public alert:AlertService = new AlertService();
  public filters:Filter[]=[];
  public cars:Car[] = [];
  
  filteredCars:Car[];
  
  constructor(public service:CarsService) {
    service.ngOnInit();
    
    this.service.GetCars().subscribe(data => {
   
      if(data.length!=0){
        
        this.cars=data;
        this.cars.forEach(c => {
          c.Reports.forEach(r => {
            r.CreatedDate = new Date(r.CreatedDate);
            r.ButtonText =  "Показать комментарии";
          })
        })
        console.log(this.cars);
      
        
        
      }
      else{
       
        // this.cars.push({
        //   Id:1,
        //   Model:"VW Up",
        //   Photo:"../../assets/images/car.jpg",
        //   Passengers:5,
        //   Doors:5,
        //   Consumption:7,
        //   Transmission:"Automatic",
        //   Fuel:"Petrol",
        //   Price:28,
        //   Description:"Крутая машина, БЕРИТЕ!",
        //   Description_ENG:"The best car I've ever had! You MUST try it!",
        //   Books:[{
        //     Id:1,
        //     DateStart:new Date(2018,7,24,3,30),
        //     DateFinish:new Date(2018,7,29,3,30),
        //     UserId:1,
        //     CarId:1,
        //     Price:28,
        //     Place:"Iraklion Airport"
        //   }, {
        //     Id:1,
        //     DateStart:new Date(2018,7,24,3,30),
        //     DateFinish:new Date(2018,7,29,3,30),
        //     UserId:1,
        //     CarId:1,
        //     Price:28,
        //     Place:"Iraklion Airport"
        //   }],
        //   Reports:[{Id:1, UserId:1, CarId:1, Mark:4, Text:"The ClientApp subdirectory is a standard Angular CLI application. If you open a command prompt in that directory, you can run any ng command (e.g., ng test), or use npm to install extra packages into it.",  CreatedDate:new Date(2017,5,6,12)}, {Id:1, UserId:1, CarId:1, Mark:4, Text:"The ClientApp subdirectory is a standard Angular CLI application. If you open a command prompt in that directory, you can run any ng command (e.g., ng test), or use npm to install extra packages into it.", CreatedDate:new Date(2017,5,6,12)},{Id:1, UserId:1, CarId:1, Mark:4, Text:"The ClientApp subdirectory is a standard Angular CLI application. If you open a command prompt in that directory, you can run any ng command (e.g., ng test), or use npm to install extra packages into it.", CreatedDate:new Date(2017,5,6,12)}]
          
        // })
        
      }
      this.filteredCars=this.cars;
      this.load=false;
      
    })
  }
  
  bookCar(car:Car){
    this.service.car=car;

    this.service.showBookingForm=true;
  }
  showCarInfo(car:Car){
    this.service.car=car;

    this.service.showCarInfo=true;
  }

  get f() {return this.filters.map(x=>x.Value)}
  addFilter(name:string,value:string){
    if(this.filters.map(x=>x.Value).indexOf(value)==-1){
     
      this.filters.push({Name:name,Value:value});
      this.Filter();
    }
    else{
      this.filters.splice(this.filters.map(x=>x.Value).indexOf(value),1);
      if(this.filters.length>0){
        this.Filter();
      }
      else{
        this.filteredCars = this.cars;
      }
    }
    
  }
  
  Filter(){
    this.filteredCars=[];
    this.cars.forEach(x => {
      if(this.filters.map(x=>x.Value).indexOf(x.Transmission)>-1 ){
        this.filteredCars.push(x);
      }
    })

  }
}


