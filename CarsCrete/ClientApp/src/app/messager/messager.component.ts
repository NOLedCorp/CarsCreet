import { Component, OnInit, Input, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message, MessagerService, Topic } from '../services/MessagerService';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'messager',
  templateUrl: './messager.component.html',
  styleUrls: ['./messager.component.css']
})
export class MessagerComponent implements OnInit, OnChanges {
  showMess:boolean=false;
  
  @Input() showAll:boolean = false;
  @Input() userProfile:boolean = false;
  @Input() userId:number = 0;
  @Input() topics:Topic[] = [];
  messageForm: FormGroup;
  currentTopic:Topic = null;
  submitted = false;
  
  constructor(private messagerService: MessagerService, private formBuilder: FormBuilder, private router: Router, private ARouter: ActivatedRoute){
    // console.log(ARouter.snapshot.url);
   }
  get f() { return this.messageForm.controls; }
  get g() { return this.messageForm.controls; }
  ngOnInit() {
    if(this.topics.length==1){
      this.currentTopic = this.topics[0];
    }
    console.log(this.currentTopic);
    this.messageForm = this.formBuilder.group({
      Name: [''],
      Email: ['', Validators.required],
      Message: ['', Validators.required],
         
    });
  }
  showTopic(top?:Topic){
    if(this.currentTopic){
      this.currentTopic = null;
    }
    else{
      this.currentTopic = top;
    }
  }
  ngOnChanges(ch:SimpleChanges){
    console.log(ch);
    if(ch.topics){
      if(this.currentTopic){
        this.currentTopic = ch.topics.currentValue.find(x => x.Id = this.currentTopic.Id);
      }
    }

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
  CreateTopic(){
    this.messagerService.createTopic({
      Id:0,
      UserId:this.userId,
      UserReciverId:0,
      ModifyDate: new Date()
    }).subscribe(data => {
      data.ModifyDate = new Date(data.ModifyDate);
      
      this.topics.unshift(data);
      this.currentTopic = this.topics[0];
    })
  }
  sendMessage(message:HTMLInputElement){
    this.messagerService.saveMessage({
      Id:0,
      UserId:this.userId,
      TopicId:this.currentTopic?this.currentTopic.Id:0,
      Text:message.value,
      CreateDate: new Date()
    }).subscribe(data => {
      data.CreateDate= new Date(data.CreateDate);
      this.currentTopic.Messages.unshift(data);
      message.value="";
    })
    
  }

}
