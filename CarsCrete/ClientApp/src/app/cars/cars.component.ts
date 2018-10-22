import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {User} from '../services/UserService';
import {CarsService, Car} from '../services/CarsService';

@Component({
  selector: 'cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']

})
export class CarsComponent {
  public user:User;
  public cars:Car[];
  public carss:number[] = [1,2,3,4,5,6,7,8,9,10];
  constructor(private service:CarsService) {
    if(localStorage.getItem("currentUser")){
      this.user=JSON.parse(localStorage.getItem("currentUser"));
    }
    this.service.GetCars().subscribe(data => {
      this.cars=data;
      console.log(this.cars);
    })




  }
  addCar(){
    this.service.AddCar().subscribe(data => {
      console.log(data);
    })
  }
}


