import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'rental-policy',
  templateUrl: './rental-policy.component.html',
  styleUrls: ['./rental-policy.component.css']
})
export class RentalPolicyComponent implements OnInit {
  items:Rules[] = [
    {
      Name: "Collision damage waiver with ZERO EXCESS (CDW)",
      Description: "The renter is insured for the rental vehicle damaged by accident with no obligation to pay any excess.",
    },
    {
      Name: "Full collision damage waiver with ZERO EXCESS (FDW & WUG)",
      Description: "The renter is insured for damages caused to the underside of the vehicle, glass, tires and engine with no obligation to pay any excess. It may also be referred as Super Collision damage waiver (SCDW) or Full collision damage waiver (FCDW) or Wheels under side and glass insurance (WUG).",
    },
    {
      Name: "Theft Protection with ZERO EXCESS (TP)",
      Description: "The renter is insured in case the vehicle is stolen, except if theft occurs because of negligence. Any stolen belongings in the interior of the car (eg. cameras, travel bags or cell-phones) are not covered by any insurance.",
    },
    {
      Name: "Public Liability Insurance (PL)",
      Description: "The renter’s liability is covered to a maximum of €1.000.000 for death and bodily injuries and €1.000.000 for material damages. May also referred as PLI.",
    },
    {
      Name: "Personal accident insurance (PAI)",
      Description: "Passengers of the rental vehicle are insured for death or bodily injuries in case of accident to a maximum of €1.000.000. Driver is insured to the amount of €15.000.",
    }
  ];
  constructor() { }

  ngOnInit() {
    console.log('worker');
  }

}

export interface Rules {
  Name: string;
  Description: string;
}
