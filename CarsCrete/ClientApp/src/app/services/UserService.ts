import { Inject, Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

export class UserService {
    openForm:boolean=false;
    type:number;
    currentUser:User;
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string){
        
      }
  
    ShowForm(type?:number){
        this.openForm=!this.openForm;
        this.type=type || 0;
    }

    AddUser(user:NewUser){
        return this.http.put<User>(this.baseUrl + 'cars/add-user', { "Name": user.Name, "Email": user.Email, "Password": user.Password});
    }
    GetUser(user:EUser){
        let params = new HttpParams().set('Email', user.Email).set('Password', user.Password);
        return this.http.get<User>(this.baseUrl + 'cars/get-user', {params})
    }

}
export interface EUser{
    Email:string;
    Password:string;
}
export interface NewUser{
    Name:string;
    Email:string;
    Password:string;
}
export interface User{
    Id:number;
    Name:string;
    Email:string;
    CreatedDate:Date;
    ModifiedDate:Date;
    Reports:FeedBack[];
    Books:Book[];
}
export interface FeedBack{
    Id:number;
    UserId:number;
    CarId:number;
    Mark:number;
    Text:string;
    CreatedDate:Date;
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