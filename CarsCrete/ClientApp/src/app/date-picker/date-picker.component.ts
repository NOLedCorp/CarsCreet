import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.css']
})
export class DatePickerComponent implements OnInit {
  firstDate:Date; 
  currentMonth:string;
  currentMonthNum:number;
  currentYear:number;
  calendar:Date[][]=[];
  week:string[] = ["MON","TUE","WED","THU","FRI","SUT","SUN"];
  constructor() { }

  ngOnInit() {
    let date = new Date();
    date = new Date(date.getFullYear(),date.getMonth());
    this.currentMonth=date.toLocaleString("en-us", {month:"short"});
    this.currentMonthNum = date.getMonth();
    this.currentYear = date.getFullYear();

    
    if(date.getDay()!=1){
      date = new Date(date.getTime()-(date.getDay()-1)*86400000);
    }
    this.firstDate = date;
    this.fillCalendar();
    console.log(this.calendar);

  }
  getClass(day:Date){
    return day.getMonth()==this.currentMonthNum;
  }
  next(){
    let date = new Date(this.currentYear,this.currentMonthNum+1);
    this.currentMonth=date.toLocaleString("en-us", {month:"short"});
    this.currentMonthNum = date.getMonth();
    this.currentYear = date.getFullYear();
    
    if(date.getDay()!=1){
      date = new Date(date.getTime()-(date.getDay()-1)*86400000);
    }
    this.firstDate = date;
    console.log(this.firstDate);
    this.fillCalendar();
  }
  fillCalendar(){
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
    let date = new Date(this.currentYear,this.currentMonthNum-1);
    this.currentMonth=date.toLocaleString("en-us", {month:"short"});
    this.currentMonthNum = date.getMonth();
    this.currentYear = date.getFullYear();
    if(date.getDay()!=1){
      date = new Date(date.getTime()-(date.getDay()-1)*86400000);
    }
    this.firstDate = date;
    this.fillCalendar();
  }

}
