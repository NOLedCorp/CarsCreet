import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CarsService, Car } from '../services/CarsService';
import { AlertService } from '../services/alertService';

@Component({
  selector: 'booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.css']
})
export class BookingFormComponent implements OnInit {
  bookingForm: FormGroup;
  submitted = false;
  alertService:AlertService = new AlertService();
  @Input() service:CarsService;
  @Input() cars:Car[];
  @Input() alert:AlertService;
  constructor(private formBuilder: FormBuilder) { }
  get f() { return this.bookingForm.controls; }
  
    onSubmit() {
      this.submitted=true;
      if (this.bookingForm.invalid) {
        return;
      }
      if(!this.service.getAvalibleCar(this.cars, this.bookingForm.value.DateStart, this.bookingForm.value.DateFinish)){
        this.alert.showA({type:'wrong',message:'Дата бронирования занята.',show:true});
      }
      this.service.BookCar(this.bookingForm.value).subscribe(data => {
        this.alert.showA({type:'success',message:'Время успешно забронированно.',show:true});
        
      })

    }
    hide(){
      this.service.showBookingForm=false;
    }
  ngOnInit() {
    this.bookingForm = this.formBuilder.group({
      Name: ['', Validators.required],
      Email: ['', Validators.required],
      Password: ['', Validators.required],
      DateStart:['', Validators.required],
      DateFinish:['', Validators.required],
      Place:['', Validators.required],
      Comment:['',Validators.required]
    });
  }

}
