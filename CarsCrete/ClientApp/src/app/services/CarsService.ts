import { Inject, Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {FeedBack} from '../services/UserService';

export class CarsService{
    showCarInfo:boolean=false;
    showBookingForm:boolean = false;
    public car:Car=null;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){

    }
    GetCars(){
        return this.http.get<Car[]>(this.baseUrl + 'cars/get-cars');
    }
    AddCar(){
        return this.http.put<Car>(this.baseUrl + 'cars/add-car',{});
    }
    getAvalibleCar(cars:Car[], DateStart:Date, DateFinish:Date){
       
    }
    BookCar(book:Book){
        return this.http.put<Book>(this.baseUrl + 'cars/add-booking',{"DateStart":book.DateStart, "DateFinish":book.DateFinish, "UserId":book.UserId, "CarId":book.CarId, "Price":book.Price, "Place":book.Place});
    }
}

export interface Car{
    Id:number;
    Model:string;
    Photo:string;
    Passengers:number;
    Doors:number;
    Transmission:string;
    Fuel:string;
    Consumption:string;
    Description:string;
    Description_ENG:string;
    Price:number;
    Reports:FeedBack[];
}

export interface Book{
    Id:number;
    DateStart:Date;
    DateFinish:Date;
    UserId:number;
    CarId:number;
    Price:number;
    Place:string;
}

export interface Filter{
    Name:string;
    Value:string;
} 