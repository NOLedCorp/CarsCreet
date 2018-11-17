import { Inject, Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Car } from '../services/CarsService';
import { Topic } from './MessagerService';

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
        return this.http.put<User>(this.baseUrl + 'cars/add-user', { "Name": user.Name, "Email": user.Email, "Password": user.Password, "Phone": user.Tel, "Lang":user.Lang});
    }
    GetUser(user:EUser){
        let params = new HttpParams().set('Email', user.Email).set('Password', user.Password);
        return this.http.get<User>(this.baseUrl + 'cars/get-user', {params})
    }
    GetUserById(id:number){
        
        return this.http.get<User>(this.baseUrl + 'cars/get-user-by-id/'+id)
    }
    UploadPhoto(data:any){
        return this.http.post(this.baseUrl + 'cars/upload-user-photo', {"uploadedFile":data})
    }
    GetStatistics(){
        return this.http.get<Statistics>(this.baseUrl + 'cars/get-statistics')
    }
    ChangeInfo(type:string, value:string, userId:number){
        return this.http.post<boolean>(this.baseUrl + 'cars/change-info', { "Type": type, "Value": value, "UserId":userId});
    }

}
export interface Statistics{
    Cars:Car[];
    Users:User[];
    Books:Book[];
    Reports:FeedBack[];
}
export interface EUser{
    Email:string;
    Password:string;
}
export interface NewUser{
    Name:string;
    Email:string;
    Password:string;
    Tel:string;
    Lang:string;
}
export interface User{
    Id:number;
    Name:string;
    Email:string;
    Phone:string;
    Lang:string;
    Topics?:Topic[];
    CreatedDate:Date;
    ModifiedDate:Date;
    Reports:FeedBack[];
    Books:Book[];
}
export interface FeedBack{
    Id:number;
    UserId:number;
    CarId:number;
    Look:number;
    Drive:number;
    Comfort:number;
    Likes:Like[];
    Mark:number;
    Text:string;
    CreatedDate:Date;
    User:ReportUser;
    Car:Car;
    Comments:ReportComment[];
    ShowForm:boolean;
    ShowComments:boolean;
    ButtonText:string;
}

export interface ReportComment{
    Id:number;
    UserId:number;
    FeedBackId:number;
    Likes:Like[];
    Text:string;
    CreatedDate:Date;
    User:ReportUser;

}
export class ReportUser{
    id:number;
    Name:string;
    Photo:string;
}
export interface Book{
    Id:number;
    DateStart:Date;
    DateFinish:Date;
    CreateDate:Date;
    UserId:number;
    CarId:number;
    Price:number;
    Place:string;
    Car:Car;
}

export interface Like{
    Id:number;
    UserId:number;
    FeedBackId:number;
    CommentId:number;
    IsLike:boolean;
}

export class Sale{
    Id:number;
    CarId:number;
    DateStart:Date;
    DateFinish:Date;
    Discount:number;
    Type:number;
    NewPrice:number;
    DaysNumber:number;
}

export class ShowSale{
    Model:string;
    NewPrice:number;
    Id:number;
}