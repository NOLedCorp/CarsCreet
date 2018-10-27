import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

@Component({
  selector: 'picker',
  templateUrl: './picker.component.html',
  styleUrls: ['./picker.component.css']
})
export class PickerComponent implements OnInit, OnChanges {
  @Input() items:any;
  @Input() out:any;
  @Input() prop:string;
  activeItem:any;
  open:boolean;
  constructor() { }

  ngOnInit() {
    this.activeItem = undefined;
    this.open=false;
  }
  ngOnChanges(changes: SimpleChanges) {
    
    if(changes.out){ 
      if(changes.out.currentValue[this.prop]==0){
        console.log("1111");
        this.ngOnInit();
      }
    }
    
  }
  choose(item:any){
    this.activeItem=item;
    this.out[this.prop]=item.Id;
    this.open = !this.open;
  }
  openCars(){
    this.open=!this.open;
  }

}
