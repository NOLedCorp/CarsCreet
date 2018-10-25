import { HttpClient } from '@angular/common/http';
import { Component, Inject, Injectable } from '@angular/core';
import {User} from '../services/UserService';
import {CarsService, Car, Filter, Book} from '../services/CarsService';
import { Observable } from 'rxjs';


@Injectable()
export class FeedBackService{
  reports:Report[];
  curReports:Report[];
  buttons:any = [];
  number:number=0;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
    
  }
  getReports(){
    this.reports=[];
    this.number=0;
    this.http.get<Report[]>(this.baseUrl + 'cars/get-reports').subscribe(data => {
      this.reports=data;
      this.reports.forEach(r => {
          r.CreateDate = new Date(r.CreateDate);
      })
      this.number=this.reports.length;
      this.changePage(0,21);

    })
  }
  saveReport(report:any){
    
    return this.http.post<Report>(this.baseUrl + 'api/quest/savereport', { "Name": report.firstName, "Surname": report.sur, "Text":report.report, "Skill": report.skill, "Mark": report.mark, "VisitTime": new Date(report.date) });
  }
  
  changePage(floor:number, top:number){
    this.curReports = [];
    for(let i = floor;i<top;i++){
      if(i<this.number){
        this.curReports.push(this.reports[i]);
      }
      else{
        break;
      }
      
    }

  }
}



export interface Report {
    Car:Car;
    User:User;
    Id: number;
    UserId: number;
    CarId: number;
    Mark: number;
    Text:string;
    CreateDate: Date;
}



