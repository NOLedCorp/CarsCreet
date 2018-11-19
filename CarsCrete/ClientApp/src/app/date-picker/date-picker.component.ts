import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.css']
})
export class DatePickerComponent implements OnInit {
  firstDate:Date; 
  calendar:Date[][]=[];
  constructor() { }

  ngOnInit() {
    let date = new Date();
    date = new Date(date.getFullYear(),date.getMonth());
    if(date.getDay()!=1){
      date = new Date(date.getTime()-(date.getDay()-1)*86400000);
    }
    this.firstDate = date;
    for(let i =0; i<5;i++){
      let d = [];
      for(let j = 0; j<7;j++){
        d.push(new Date(this.firstDate.getTime()+86400000*i*7+86400000*j));
      }
      this.calendar.push(d);
    }
    console.log(this.calendar);

  }

}
