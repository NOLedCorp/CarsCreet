import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Message } from '../services/MessagerService';
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
  @Input() messages:Message[] = [
    {
      UserId: 7,
      Text: 'Привет!',
      UserReciverId: 10,
      CreateDate: new Date(2018,10,10,4,30, 15)
    },
    {
      UserId: 7,
      Text: 'Привет!',
      UserReciverId: 10,
      CreateDate: new Date(2018,10,10,4,29, 15)
    },
    {
      UserId: 10,
      Text: 'Товарищи! консультация с широким активом позволяет выполнять важные задания по разработке систем массового участия. Не следует, однако забывать, что дальнейшее развитие различных форм деятельности способствует подготовки и реализации форм развития. Идейные соображения высшего порядка, а также дальнейшее развитие различных форм деятельности позволяет оценить значение новых предложений.',
      UserReciverId: 7,
      CreateDate: new Date(2018,10,10,4,28, 15)
    },
    {
      UserId: 7,
      Text: 'Ceteros assentior omittantur cum ad. Odio contentiones sed cu, usu commodo prompta prodesset id. Mandamus abhorreant deseruisse mea at, mea elit deserunt persequeris at, in putant fuisset honestatis qui. Lorem ipsum dolor sit amet, an eos lorem ancillae expetenda, vim et utamur quaestio. Mandamus abhorreant deseruisse mea at, mea elit deserunt persequeris at, in putant fuisset honestatis qui. Tation delenit percipitur at vix. . An eos iusto solet, id mel dico habemus. Elitr accommodare deterruisset eam te, vim munere pertinax consetetur at.',
      UserReciverId: 10,
      CreateDate: new Date(2018,10,10,4,27, 15)
    },
    {
      UserId: 7,
      Text: 'Привет!',
      UserReciverId: 10,
      CreateDate: new Date(2018,10,10,4,26, 15)
    },
    {
      UserId: 10,
      Text: 'Привет!',
      UserReciverId: 7,
      CreateDate: new Date(2018,10,10,4,25, 15)
    },
    {
      UserId: 7,
      Text: 'Привет!',
      UserReciverId: 10,
      CreateDate: new Date(2018,10,10,4,24, 15)
    },
    {
      UserId: 7,
      Text: 'Привет!',
      UserReciverId: 10,
      CreateDate: new Date(2018,10,10,4,23, 15)
    },
    {
      UserId: 7,
      Text: 'Привет!',
      UserReciverId: 10,
      CreateDate: new Date(2018,10,10,4,22, 15)
    }
  ];
  messageForm: FormGroup;
  submitted = false;
  
  constructor(private formBuilder: FormBuilder, private router: Router, private ARouter: ActivatedRoute){
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
    this.messages.unshift({
      UserId:this.userId,
      UserReciverId:10,
      Text:message.value,
      CreateDate: new Date()
    })
  }

}
