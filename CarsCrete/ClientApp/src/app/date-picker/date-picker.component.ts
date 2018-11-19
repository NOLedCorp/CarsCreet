import { Component, OnInit, Input, SimpleChange, SimpleChanges } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.css']
})
export class DatePickerComponent implements OnInit {
  @Input() Out:any = new Ex();
  @Input() Prop:string = "DateStart";
  showPicker:boolean = false;
  firstDate:Date; 
  currentMonth:string;
  currentMonthNum:number;
  currentYear:number;
  weekStart:number;
  calendar:Date[][]=[];
  week:string[] = ["MON","TUE","WED","THU","FRI","SUT","SUN"];
  constructor( public translate:TranslateService) { }

  ngOnInit() {
    
    let date = new Date();
    date = new Date(date.getFullYear(),date.getMonth());
    this.currentMonth=date.toLocaleString("en-us", {month:"short"});
    this.currentMonthNum = date.getMonth();
    this.currentYear = date.getFullYear();
    
    if(date.getDay()!=this.weekStart){
      date = new Date(date.getTime()-(date.getDay()-1)*86400000);
    }
    this.firstDate = date;
    this.fillCalendar();
    this.translate.onLangChange.subscribe(d => {
      if(d.lang=="ru"){
        this.weekStart=1;
        this.week = ["MON","TUE","WED","THU","FRI","SUT","SUN"];
      }
      else{
        this.week = ["SUN","MON","TUE","WED","THU","FRI","SUT"]
        this.weekStart=0;
      }
      this.setMonth(0);
      
    })

  }
  getClass(day:Date){
    return day.getMonth()==this.currentMonthNum;
  }
  next(){
    this.setMonth(1);
  }
  fillCalendar(){
    console.log(this.weekStart);
    
    this.calendar=[];
    for(let i =0; i<5;i++){
      let d = [];
      for(let j = 0; j<7;j++){
        d.push(new Date(this.firstDate.getTime()+86400000*i*7+86400000*j));
      }
      this.calendar.push(d);
    }
  }
  prev(){
    this.setMonth(-1);
  }
  setMonth(c:number){
    let date = new Date(this.currentYear,this.currentMonthNum+c);
    this.currentMonth=date.toLocaleString("en-us", {month:"short"});
    this.currentMonthNum = date.getMonth();
    this.currentYear = date.getFullYear();
    if(date.getDay()!=this.weekStart){
      date = new Date(date.getTime()-(date.getDay()-this.weekStart)*86400000);
    }
    this.firstDate = date;
    this.fillCalendar();
  }
  pick(date:Date){
    this.Out[this.Prop]=date;
    console.log(this.Out[this.Prop]==date);
    this.hide();
  }

  hide(){
    this.showPicker = !this.showPicker;
  }
}

 export class Ex{
   DateStart:Date;
 }