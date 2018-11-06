import { Component, OnInit } from '@angular/core';
import { worker } from 'cluster';

@Component({
  selector: 'rental-policy',
  templateUrl: './rental-policy.component.html',
  styleUrls: ['./rental-policy.component.css']
})
export class RentalPolicyComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    console.log('worker');
  }

}
