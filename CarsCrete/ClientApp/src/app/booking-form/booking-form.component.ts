import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CarsService, Car, Book } from '../services/CarsService';
import { AlertService } from '../services/AlertService';
import {User} from '../services/UserService';

@Component({
  selector: 'booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.css']
})
export class BookingFormComponent implements OnInit {
  bookingForm: FormGroup;
  submitted = false;
  public book:Book;
  public user:User;
  alertService:AlertService = new AlertService();
  @Input() service:CarsService;
  @Input() alert:AlertService;
  constructor(private formBuilder: FormBuilder) { }
  get f() { return this.bookingForm.controls; }
  
    onSubmit() {
      this.submitted=true;
      if (this.bookingForm.invalid) {
        return;
      }
     
      if(localStorage.getItem("currentUser")){
        this.user=JSON.parse(localStorage.getItem("currentUser"));
        this.book = {
          Id:null,
          CarId:this.service.car.Id,
          UserId:this.user.Id,
          DateStart:this.bookingForm.value.DateStart,
          DateFinish:this.bookingForm.value.DateFinish,
          Price:this.service.car.Price*(this.bookingForm.value.DateFinish - this.bookingForm.value.DateStart),
          Place:"Iraklion airport",
          Tel:this.bookingForm.value.Tel,
          Comment:this.bookingForm.value.Comment
        }
        this.service.BookCar(this.book).subscribe(data => {
          this.alert.showA({type:'success',message:'Время успешно забронированно.',show:true});
          console.log(data);
        })
      }
      else{
        this.book = {
          Id:null,
          CarId:this.service.car.Id,
          UserId:123,
          DateStart:this.bookingForm.value.DateStart,
          DateFinish:this.bookingForm.value.DateFinish,
          Price:this.service.car.Price,
          Place:"Iraklion airport",
          Email:this.bookingForm.value.Email,
          Password:this.bookingForm.value.Password,
          Name:this.bookingForm.value.Name,
          Tel:this.bookingForm.value.Tel,
          Comment:this.bookingForm.value.Comment

        }
        this.service.BookCarNew(this.book).subscribe(data => {
          console.log(data);
          this.alert.showA({type:'success',message:'Время успешно забронированно.',show:true});
          
        })
      }
    }
    hide(){
      this.service.showBookingForm=false;
    }
    ngOnInit() {
      this.bookingForm = this.formBuilder.group({
        Name: ['', Validators.required],
        Email: ['', Validators.required],
        Password: ['', Validators.required],
        Tel: [''],
        DateStart:['', Validators.required],
        DateFinish:['', Validators.required],
        Place:['', Validators.required],
        Comment:['']
      });
  }

}
