import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '../services/AlertService';
import { FeedBackService, Report } from '../services/FeedBackService';
import { ReportComment } from '../services/UserService';

@Component({
  selector: 'feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.css']
  
})
export class FeedbackComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  
  @Input() reports:Report[]=[];

  buttons:boolean[] = [];
  alertService = new AlertService(); 
  
  constructor(private formBuilder: FormBuilder, public feedBackService:FeedBackService){}

  
  ngOnInit() {
    if(false){
      this.feedBackService.getReports();
    }
    else{
      this.feedBackService.reports=this.reports;
      this.feedBackService.number=this.reports.length;
      console.log(this.reports);
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
  }
  addLikes(comment:any, k:boolean, report:boolean){
 
    if(k){
      comment.Likes+=1;
      this.feedBackService.addLikeOrDislike(comment.Id, true, report).subscribe(data => {
        console.log(data);
      })
    }
    else{
      console.log(comment);
      comment.Dislikes+=1;
      this.feedBackService.addLikeOrDislike(comment.Id, k, report).subscribe(data => {
        console.log(data);
      })
    }
    
  }
  showForm(com:Report){
    console.log(com);
    com.ShowForm=!com.ShowForm;
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

