import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../services/AlertService';

import { FeedBackService } from '../services/FeedBackService';
import { ReportComment, UserService, FeedBack } from '../services/UserService';

@Component({
  selector: 'feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
  
})
export class FeedbackComponent implements OnInit {
  registerForm: FormGroup;
  commentForm: FormGroup;
  submitted = false;
  
  @Input() reports:FeedBack[]=[];

  buttons:boolean[] = [];
  alertService = new AlertService(); 
  
  constructor(private formBuilder: FormBuilder, public feedBackService:FeedBackService, public userService:UserService){}

  
  ngOnInit() {
    if(false){
      this.feedBackService.getReports();
    }
    else{
      this.feedBackService.reports=this.reports;
      this.feedBackService.number=this.reports.length;
 
      this.feedBackService.changePage(0,21);
    }
    
    
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      date: ['', Validators.required],
      sur:[''],
      mark:['', Validators.required],
      skill: [''],
      report: ['', [Validators.required]]
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
  showForm(com:FeedBack){
    if(localStorage.getItem("currentUser")){
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
      if (this.registerForm.invalid) {  
         return;
      }
      
      this.feedBackService.saveReport(this.registerForm.value).subscribe(data => {
        if(data) {
          this.feedBackService.getReports();
          
          this.registerForm.reset();
          this.alertService.showA({type:'success',message:'Комментарий успешно оставлен.',show:true});
        }
      },error => console.log(error))
      
      
      this.submitted=false;
        
        
       
    }

}

