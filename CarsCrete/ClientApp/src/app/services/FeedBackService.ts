import { HttpClient } from '@angular/common/http';
import { Component, Inject, Injectable } from '@angular/core';
import {User, ReportComment, FeedBack} from '../services/UserService';
import {CarsService, Car, Filter, Book} from '../services/CarsService';
import { Observable } from 'rxjs';


@Injectable()
export class FeedBackService{
  reports:FeedBack[];
  curReports:FeedBack[];
  buttons:any = [];
  number:number=0;


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
    
  }
  getReports(){
    this.reports=[];
    this.number=0;
    this.http.get<FeedBack[]>(this.baseUrl + 'cars/get-reports').subscribe(data => {
      this.reports=data;
      this.reports.forEach(r => {
          r.CreatedDate = new Date(r.CreatedDate);
      })
      this.number=this.reports.length;
      this.changePage(0,21);

    })
  }
  saveReport(report:any){
    
    return this.http.post<FeedBack>(this.baseUrl + 'api/quest/savereport', { "Name": report.firstName, "Surname": report.sur, "Text":report.report, "Skill": report.skill, "Mark": report.mark, "VisitTime": new Date(report.date) });
  }
  addLikeOrDislike(id:number,type:boolean, report:boolean){
    return this.http.post<FeedBack>(this.baseUrl + 'cars/add-likes', { "Type": type, "CommentId": id, "Report":report});
  }
  addComment(text:string, UserId:number, FeedBackId:number ){
    return this.http.put<ReportComment>(this.baseUrl + 'cars/add-comment',{"Text":text, "UserId":UserId, "FeedBackId":FeedBackId});
  }
  changePage(floor:number, top:number){
    this.curReports = [];
    
    for(let i = floor;i<top;i++){
      if(i<this.number){
        this.curReports.push(this.reports[i]);
        this.reports[i].ShowForm=false;
      }
      else{
        break;
      }
      
    }

  }
}



// export interface Report {
//     Car:Car;
//     User:User;
//     Id: number;
//     UserId: number;
//     CarId: number;
//     Mark: number;
//     Text:string;
//     CreateDate: Date;
//     ShowForm:boolean;

// }



