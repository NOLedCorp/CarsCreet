import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CarsService, Car, Book } from '../services/CarsService';
import { AlertService } from '../services/AlertService';
import {User} from '../services/UserService';
import { ActivatedRoute } from "@angular/router";
import {TranslateService} from '@ngx-translate/core';

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
  
  
  constructor(public translate: TranslateService,private formBuilder: FormBuilder,private route: ActivatedRoute, public service:CarsService, public alert:AlertService) { }
  get f() { return this.bookingForm.controls; }
  
    onSubmit(ds:HTMLInputElement, df:HTMLInputElement) {
      this.submitted=true;
      if (this.bookingForm.invalid) {
        return;
      }
     
      if(localStorage.getItem("currentUser")){
        this.user=JSON.parse(localStorage.getItem("currentUser"));
        this.book = {
          Id:123,
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
          Id:123,
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
          
        },error => {
          console.clear();
          if(error.status==501){

            this.alert.showA({type:'wrong',message:'Время забронированно',show:true});
            this.submitted=false;
            df.value="";
            ds.value="";
            this.bookingForm.value.DateStart="";
            
          }
          else{
            this.alert.showA({type:'wrong',message:'Неверный пароль',show:true});
            this.submitted=false;
          }
        }
        )
      }
    }
    hide(){
      this.service.showBookingForm=false;
    }
    ngOnInit() {
      this.service.GetCar(this.route.snapshot.paramMap.get("id")).subscribe(data => {
       
        if(data){
        
          this.service.car=data;
          
        }
        else{
         
          this.service.car= {
            Id:1,
            Model:"VW Up",
            Photo:"../../assets/images/car.jpg",
            Passengers:5,
            Doors:5,
            Consumption:7,
            Transmission:"Automatic",
            Fuel:"Petrol",
            Price:28,
            Description:"Крутая машина, БЕРИТЕ!",
            Description_ENG:"The ClientApp subdirectory is a standard Angular CLI application. If you open a command prompt in that directory, you can run any ng command (e.g., ng test), or use npm to install extra packages into it."}, {Id:1, UserId:1, CarId:1, Mark:4, Text:"The ClientApp subdirectory is a standard Angular CLI application. If you open a command prompt in that directory, you can run any ng command (e.g., ng test), or use npm to install extra packages into it."},{Id:1, UserId:1, CarId:1, Mark:4, Text:"The ClientApp subdirectory is a standard Angular CLI application. If you open a command prompt in that directory, you can run any ng command (e.g., ng test), or use npm to install extra packages into it.",
            Books:[{
              Id:1,
              DateStart:new Date(2018,7,24,3,30),
              DateFinish:new Date(2018,7,29,3,30),
              UserId:1,
              CarId:1,
              Price:28,
              Place:"Iraklion Airport"
            }, {
              Id:1,
              DateStart:new Date(2018,7,24,3,30),
              DateFinish:new Date(2018,7,29,3,30),
              UserId:1,
              CarId:1,
              Price:28,
              Place:"Iraklion Airport"
            }],
            Reports:[{Id:1, UserId:1, CarId:1, Mark:4, Text:"The ClientApp subdirectory is a standard Angular CLI application. If you open a command prompt in that directory, you can run any ng command (e.g., ng test), or use npm to install extra packages into it."}, {Id:1, UserId:1, CarId:1, Mark:4, Text:"The ClientApp subdirectory is a standard Angular CLI application. If you open a command prompt in that directory, you can run any ng command (e.g., ng test), or use npm to install extra packages into it."},{Id:1, UserId:1, CarId:1, Mark:4, Text:"The ClientApp subdirectory is a standard Angular CLI application. If you open a command prompt in that directory, you can run any ng command (e.g., ng test), or use npm to install extra packages into it."}]
            
          }
        console.log(data);
      }})
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
