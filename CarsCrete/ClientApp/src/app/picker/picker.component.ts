import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'picker',
  templateUrl: './picker.component.html',
  styleUrls: ['./picker.component.css']
})
export class PickerComponent implements OnInit {
  @Input() items:any;
  @Input() out:any;
  @Input() prop:string;
  activeItem:any;
  open:boolean=false;
  constructor() { }

  ngOnInit() {
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
