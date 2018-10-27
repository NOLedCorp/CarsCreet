import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {
  @Input() mark:number;
  @Input() markOut:any;
  @Input() prop:string;
  stars:Star[]=[{Id:0, Type:0},{Id:1, Type:0},{Id:2, Type:0},{Id:3, Type:0},{Id:4, Type:0}];
  Mark:number = 0;
  constructor() { 
    
  }

  ngOnInit() {
    if(this.mark>0){
      this.Mark = this.mark;
      let i = 0;
      while(this.mark>0){
        this.stars[i].Type=1
        if(this.mark<1){
          this.stars[i].Type=2
        }
        i+=1;
        this.mark-=1;
      }
    }
    else{
      
    }
    

  }
  setRating(star: Star){
    this.markOut[this.prop]=star.Id+1;
  }
  changeRating(star:Star, k?:true){
    if(this.prop){
      if(!k){
        for(let i=0; i<5;i++){
          
          if(i<star.Id+1){
            this.stars[i].Type=1;
            this.Mark=i+1;
          }
          else{
            this.stars[i].Type=0;
          }
          
          
        }
      }
      else{
  
        if(this.markOut[this.prop]==0){
          this.Mark=0;
          for(let i=0; i<5;i++){
            if(i<star.Id+1){
              this.stars[i].Type=0;
            }
            
          }
        }
        else{
          star.Id=this.markOut[this.prop]-1;
          this.changeRating(star)
        }
      }
    }
    
  }

}

export interface Star{
  Id:number;
  Type:number;
}
