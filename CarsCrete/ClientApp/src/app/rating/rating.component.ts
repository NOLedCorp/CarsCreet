import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {
  @Input() mark:number;
  stars:number[]=[0,0,0,0,0];
  Mark:number = 0;
  constructor() { 
    
  }

  ngOnInit() {
    this.Mark = this.mark;
    let i = 0;
    while(this.mark>0){
      this.stars[i]=1
      if(this.mark<1){
        this.stars[i]=2
        
      }
      i+=1;
      this.mark-=1;
    }

  }

}
