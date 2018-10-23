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
  public cars:Car[];
  
  filteredCars:Car[];
  
  constructor(public service:CarsService) {
    service.ngOnInit();
    
    this.service.GetCars().subscribe(data => {
      
      console.log(data);
      this.cars=data;
      this.filteredCars=data;
      this.load=false;
    })
  }
  
  bookCar(car:Car){
    this.service.car=car;
    console.log(car);
    this.service.showBookingForm=true;
  }
  showCarInfo(car:Car){
    this.service.car=car;
    console.log(car);
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


