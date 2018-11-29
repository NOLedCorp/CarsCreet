import { Inject, Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import {FeedBack, Sale} from '../services/UserService';

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
    GetSameCars(id){
        return this.http.get<Car[]>(this.baseUrl + 'cars/get-same-cars/'+id);
    }
    GetCar(id:string){
        return this.http.get<Car>(this.baseUrl+'cars/get-car/'+id);
    }
    GetCarPhotos(id:number){
        return this.http.get<string[]>(this.baseUrl+'cars/get-photos/'+id);
    }
    GetReportCars(){
        return this.http.get<ReportCar[]>(this.baseUrl+'cars/get-report-cars');
    }
    AddCar(car:NewCar){
        return this.http.put<NewCar>(this.baseUrl + 'cars/add-car',car);
    }
    
    BookCar(book:Book){
        return this.http.put<Book>(this.baseUrl + 'cars/add-booking',{"Id":123,"DateStart":book.DateStart, "ExtraDateStart":book.ExtraDateStart, "DateFinish":book.DateFinish, "UserId":book.UserId, "CarId":book.CarId, "Price":book.Price, "Place":book.Place, "Comment":book.Comment, "SalesId":book.SalesId});
        
    }
    BookCarNew(book:Book){
        return this.http.put<Book>(this.baseUrl + 'cars/add-booking-new', {"Id":123,"DateStart":book.DateStart,"ExtraDateStart":book.ExtraDateStart, "DateFinish":book.DateFinish,  "CarId":book.CarId, "Price":book.Price, "Place":book.Place,"Email":book.Email, "Password":book.Password, "Name":book.Name, "SalesId":book.SalesId, "Phone":book.Tel, "Comment":book.Comment});
    }
    GetSales(){
        return this.http.get<Sale[]>(this.baseUrl+'cars/get-sales');
    }

    ngOnInit(){
        
    }
    checkStr(str:string, type?:string){
        if(!type){
            var reg = /(\s??хер|[а-я]*ху[ей]+|пид[оа]р[а-я]*|суч?ка|[пзд]?[оа]?[ел]б[ауеоё][еёлчнтмш][а-я]*|бл[яе]а?[тдь]{0,2}|[расзпо]*пизд[ецаитьуняй]*)/gi
            str = str.replace(reg,"***");
            
        }
        if(type = 'phone'){
            var reg = /\D/g;
            
            str = str.replace(reg,"");
        }
        if(type = 'phone-check'){
            var reg = /(^\+?\d{1,3}?[- .]?\(?(?:\d{2,3})\)?[- .]?\d\d\d[- .]?\d\d\d\d$|^((\+?7|8)[ \-] ?)?((\(\d{3}\))|(\d{3}))?([ \-])?(\d{3}[\- ]?\d{2}[\- ]?\d{2})$)/g;
            
            str = str.replace(/\D/g,"");
            console.log(str.match(reg));
            return str.match(reg)?str.match(reg)[0]:null;
        }
        return str;
    }
    checkEmail(str:string){
        return !str.match(/[a-z]+@[a-z]+\.[a-z]+/ig);
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
    BodyType:string;
    FilterProp:number;
    AC:boolean;
    ABS:boolean;
    Radio:boolean;
    Airbags:boolean;
    Description:string;
    Description_ENG:string;
    Price:number;
    Reports:FeedBack[];
    Books:Book[];
    Sales:Sale[];
    Includes:string[];
    IncludesEng:string[];
}
export class NewCar{
    Id:number = 0;
    Model:string = null;
    Photo:string = null;
    Contain:string = "";
    Passengers:number = null;
    Doors:number = null;
    Transmission:string = null;
    Fuel:string = null;
    Consumption:number = 0;
    BodyType:string = null;
    FilterProp:number = 0;
    AC:boolean = false;
    ABS:boolean = false;
    Radio:boolean = false;
    Airbags:boolean = false;
    Description:string = null;
    Description_ENG:string = null;
    Price:number = null;
    Includes:string[] = [];
}
export interface BookTimes{
    CarId:number;
    DateStart:Date;
    DateFinish:Date;
}
export class Book{
    Id:number;
    DateStart:Date;
    ExtraDateStart:Date = new Date(0);
    DateFinish:Date;
    Sum:number;
    UserId:number;
    CarId:number;
    SalesId?:number;
    OldPrice?:number;
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
    Price:number;
} 
export interface Filter{
    Name:string;
    Value:string;
} 

export class Contains{
    public Includes:string[] = ["Полностью комбинированное страхование",
    "Неограниченный километраж",
    "Второй водитель бесплатно",
    "Доставка/возврат в любое время",
    "Дорожная карта в подарок",
    "Доставка в аэропорт Ираклиона",
    "Аренда машины на Крите без франшизы"];
    public IncludesEng:string[] = ["Fully comprehensive insurance",
    "Unlimited mileage",
    "Second driver free of charge",
    "Delivery/return at any time",
    "Road map as a gift",
    "Delivery to Heraklion airport",
    "Rent a car in Crete with no excess"];
}