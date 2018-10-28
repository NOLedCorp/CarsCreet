import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../services/AlertService';

import { CarsService, ReportCar } from '../services/CarsService';
import { FeedBackService, ShortFeedBack } from '../services/FeedBackService';
import { ReportComment, UserService, FeedBack } from '../services/UserService';

@Component({
  selector: 'feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
  
})
export class FeedbackComponent implements OnInit {
  autorized:boolean=false;
  registerForm: FormGroup;
  choosedCar:ReportCar;
  commentForm: FormGroup;
  errors:string[]=[];
  submitted = false;
  cars:ReportCar[]=[];
  feedBack:ShortFeedBack= new ShortFeedBack();
  
  @Input() reports:FeedBack[]=[];
  @Input() type:string='all';

  buttons:boolean[] = [];
  alertService = new AlertService(); 
  
  constructor(private carsService:CarsService, private formBuilder: FormBuilder, public feedBackService:FeedBackService, public userService:UserService){}

  
  ngOnInit() {
    console.log(this.reports);
    if(localStorage.getItem("currentUser")){

      this.autorized=true;
    }
    this.carsService.GetReportCars().subscribe( data => {
      this.cars=data;
  
    })
    if(this.reports.length==0){
      if(this.type=='all'){
        this.feedBackService.getReports();
      }
      
    
    }
    else{
      this.feedBackService.reports=this.reports;
      this.feedBackService.number=this.reports.length;
 
      this.feedBackService.changePage(0,21);
    }
    
    
    this.registerForm = this.formBuilder.group({
      DateStart: ['', Validators.required],
      Report: ['', [Validators.required]]
    });
    this.commentForm = this.formBuilder.group({
      report:['', Validators.required]
    });
  }
  addLikes(comment:any, k:boolean, report:boolean){
 
    if(k){
      comment.Likes+=1;
      this.feedBackService.addLikeOrDislike(comment.Id, true, report).subscribe(data => {

      })
    }
    else{

      comment.Dislikes+=1;
      this.feedBackService.addLikeOrDislike(comment.Id, k, report).subscribe(data => {

      })
    }
    
  }
  showComments(com:FeedBack){
    if(com.ShowComments){
      com.ShowComments=!com.ShowComments;
      com.ButtonText="Показать комментарии";
    }
    else{
      com.ShowComments=!com.ShowComments;
      com.ButtonText="Скрыть комментарии";
    }

  }
  showForm(com:FeedBack){
    if(this.autorized){
      this.userService.currentUser=JSON.parse(localStorage.getItem("currentUser"));
      com.ShowForm=!com.ShowForm;
    }
    else{
      this.userService.ShowForm(0);
    }
    this.submitted=false;
    
  }
  addComment(text:string, report:FeedBack){
    this.submitted = true;
    if (this.commentForm.invalid) {  
        return;
    }
    this.feedBackService.addComment(text, this.userService.currentUser.Id, report.Id).subscribe(data =>{
      report.ShowForm=!report.ShowForm;
      this.commentForm.reset();
      report.Comments.push(data);
    })
    this.submitted=false;
  }
  get f() { return this.registerForm.controls; }
 
    onSubmit() {
        // stop here if form is invalid
      this.submitted = true;
      if(this.feedBack.CarId==0){
        this.errors.push("carid");
      }
      else{
        if(this.errors.indexOf("carid")>-1){
          this.errors.splice(this.errors.indexOf("carid"),1);
        }
      }
      if(this.feedBack.Look==0){
        this.errors.push("look");
      }
      else{
        if(this.errors.indexOf("look")>-1){
          this.errors.splice(this.errors.indexOf("look"),1);
        }
      }
      if(this.feedBack.Comfort==0){
        this.errors.push("comfort");
      }
      else{
        if(this.errors.indexOf("comfort")>-1){
          this.errors.splice(this.errors.indexOf("comfort"),1);
        }
      }
      if(this.feedBack.Drive==0){
        this.errors.push("drive");
      }
      else{
        if(this.errors.indexOf("drive")>-1){
          this.errors.splice(this.errors.indexOf("drive"),1);
        }
      }
      if (this.registerForm.invalid) {  
         return;
      }
      if(this.errors.length>0){
        return;
      }
      this.feedBack.UserId=this.userService.currentUser.Id;
      this.feedBack.DateStart=new Date(this.registerForm.value.DateStart);
      this.feedBack.Report= this.registerForm.value.Report;
   
      
      this.feedBackService.saveReport(this.feedBack).subscribe(data => {
        if(data) {
          this.feedBackService.getReports();
          this.feedBack = new ShortFeedBack();
          this.alertService.showA({type:'success',message:'Комментарий успешно оставлен.',show:true});
        }
        this.registerForm.reset();
      },error => console.log(error))
      
      
      this.submitted=false;
        
        
       
    }

}





