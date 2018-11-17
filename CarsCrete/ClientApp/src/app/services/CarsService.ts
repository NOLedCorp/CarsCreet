import { Inject, Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {FeedBack} from '../services/UserService';

export class CarsService implements OnInit{
    showCarInfo:boolean=false;
    showBookingForm:boolean = false;
    bookings:BookTimes[];
    public car:Car=null;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){
        

    }
    GetCars(){
        return this.http.get<Car[]>(this.baseUrl + 'cars/get-cars');
    }
    GetCar(id:string){
        return this.http.get<Car>(this.baseUrl+'cars/get-car/'+id);
    }
    GetReportCars(){
        return this.http.get<ReportCar[]>(this.baseUrl+'cars/get-report-cars');
    }
    AddCar(){
        return this.http.put<Car>(this.baseUrl + 'cars/add-car',{});
    }
    
    BookCar(book:Book){
        console.log(book);
        return this.http.put<Book>(this.baseUrl + 'cars/add-booking',{"Id":123,"DateStart":new Date(book.DateStart), "DateFinish":new Date(book.DateFinish), "UserId":book.UserId, "CarId":book.CarId, "Price":book.Price, "Place":book.Place, "Comment":book.Comment});
        
    }
    BookCarNew(book:Book){
        console.log(book);
        return this.http.put<Book>(this.baseUrl + 'cars/add-booking-new', {"Id":123,"DateStart":book.DateStart, "DateFinish":book.DateFinish,  "CarId":book.CarId, "Price":book.Price, "Place":book.Place,"Email":book.Email, "Password":book.Password, "Name":book.Name, "Phone":book.Tel, "Comment":book.Comment});
    }

    ngOnInit(){
        // console.log("hello");
        // this.http.get<BookTimes[]>(this.baseUrl + 'cars/get-book-times').subscribe(data => {
        //     this.bookings = data;
        //     console.log(data);
        // })
        
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
    Consumption:number;
    Description:string;
    Description_ENG:string;
    Price:number;
    Reports:FeedBack[];
    Books:Book[];
}
export interface BookTimes{
    CarId:number;
    DateStart:Date;
    DateFinish:Date;
}
export interface Book{
    Id:number;
    DateStart:Date;
    DateFinish:Date;
    UserId:number;
    CarId:number;
    SalesId?:number;
    Price:number;
    Place:string;
    Email?:string;
    Tel?:string;
    Comment?:string;
    Password?:string;
    Name?:string;
}

export interface ReportCar{
    Id:number;
    Photo:string;
    Model:string;
} 
export interface Filter{
    Name:string;
    Value:string;
} 