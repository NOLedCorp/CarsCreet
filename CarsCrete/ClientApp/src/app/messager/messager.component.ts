import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message, MessagerService } from '../services/MessagerService';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'messager',
  templateUrl: './messager.component.html',
  styleUrls: ['./messager.component.css']
})
export class MessagerComponent implements OnInit {
  showMess:boolean=false;
  
  @Input() showAll:boolean = false;
  @Input() userProfile:boolean = false;
  @Input() userId:number = 0;
  @Input() messages:Message[] = [];
  messageForm: FormGroup;
  
  submitted = false;
  
  constructor(private messagerService: MessagerService, private formBuilder: FormBuilder, private router: Router, private ARouter: ActivatedRoute){
    // console.log(ARouter.snapshot.url);
   }
  get f() { return this.messageForm.controls; }
  get g() { return this.messageForm.controls; }
  ngOnInit() {
    this.messageForm = this.formBuilder.group({
      Name: [''],
      Email: ['', Validators.required],
      Message: ['', Validators.required],
         
    });
  }
  show(show:boolean){
    
    if(!show){
      this.submitted=false;
      this.showMess=!this.showMess;
    }
    
  }
  send(){
    this.submitted=true;
    if(this.messageForm.invalid){
      return;
    }
  }
  sendMessage(message:HTMLInputElement){
    this.messagerService.saveMessage({
      Id:0,
      UserId:this.userId,
      UserReciverId:6,
      Text:message.value,
      CreateDate: new Date()
    }).subscribe(data => {
      data.CreateDate= new Date(data.CreateDate);
      this.messages.unshift(data);
      message.value="";
    })
    
  }

}
