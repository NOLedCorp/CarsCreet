import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'messager',
  templateUrl: './messager.component.html',
  styleUrls: ['./messager.component.css']
})
export class MessagerComponent implements OnInit {
  showMess:boolean=false;
  
  @Input() showAll:boolean = false;
  @Input() userProfile:boolean = false;
  messageForm: FormGroup;
  submitted = false;
  
  constructor(private formBuilder: FormBuilder){ }
  get f() { return this.messageForm.controls; }
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

}
