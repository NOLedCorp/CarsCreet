import { Inject, Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {FeedBack} from '../services/UserService';

export class CarsService{
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){

    }
    GetCars(){
        return this.http.get<Car[]>(this.baseUrl + 'cars/get-cars');
    }
    AddCar(){
        return this.http.put<Car>(this.baseUrl + 'cars/add-car',{});
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
    Price:number;
    Reports:FeedBack[];
}