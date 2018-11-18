import { Component, OnInit, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CarsService, Car, Book } from '../services/CarsService';
import { AlertService } from '../services/AlertService';
import {User, ShowSale} from '../services/UserService';
import { ActivatedRoute } from "@angular/router";
import {TranslateService} from '@ngx-translate/core';

@Component({
  selector: 'booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.css']
})
export class BookingFormComponent implements OnInit {
  showBook:boolean = false;
  bookingForm: FormGroup;
  public sales:ShowSale[];
  submitted = false;
  sale:ShowSale = {Id:0};
  res:number=0;
  rating:Raiting = {Look:0, Comfort:0, Drive:0};
  public book:Book;
  public user:User;
  
  
  constructor(public translate: TranslateService,private formBuilder: FormBuilder,private route: ActivatedRoute, public service:CarsService, public alert:AlertService) { }
  get f() { return this.bookingForm.controls; }
  Round(k:number){
    let res = Math.round(k*100)/100;
    
    return res.toFixed(2)
  }
    onSubmit(ds:HTMLInputElement, df:HTMLInputElement) {
      console.log(this.bookingForm.value);
      this.submitted=true;
      if (this.bookingForm.invalid) {
        return
        
      }
     
      if(localStorage.getItem("currentUser")){
        
        this.book = {
          Id:0,
          CarId:this.service.car.Id,
          UserId:this.user.Id,
          SalesId:this.sale.Id,
          DateStart:this.bookingForm.value.DateStart,
          DateFinish:this.bookingForm.value.DateFinish,
          Price:this.sale.Id==0?this.service.car.Price:this.sale.NewPrice,
          Place:"Iraklion airport",
          Tel:this.bookingForm.value.Tel,
          Comment:this.bookingForm.value.Comment
        }
        console.log(this.book);
        this.service.BookCar(this.book).subscribe(data => {
          this.alert.showA({type:'success',message:'Время успешно забронированно.',show:true});
         
        },error => {
          console.clear();
          if(error.status==501){

            this.alert.showA({type:'wrong',message:'Время забронированно',show:true});
            this.submitted=false;
            df.value="";
            ds.value="";
            this.bookingForm.value.DateStart="";
            this.bookingForm.value.DateFinish="";
            
          }
          else if(error.status==502){
            this.alert.showA({type:'wrong',message:'Неверный пароль',show:true});
            this.submitted=false;
          }
          else if(error.status==503 || error.status==500){
            this.alert.showA({type:'wrong',message:'Некорректные данные',show:true});
            this.submitted=false;
          }})
      }
      else{
        this.book = {
          Id:0,
          CarId:this.service.car.Id,
          UserId:0,
          SalesId:this.sale.Id,
          DateStart:this.bookingForm.value.DateStart,
          DateFinish:this.bookingForm.value.DateFinish,
          Price:this.sale.Id==0?this.service.car.Price:this.sale.NewPrice,
          Place:"Iraklion airport",
          Email:this.bookingForm.value.Email,
          Password:this.bookingForm.value.Password,
          Name:this.bookingForm.value.Name,
          Tel:this.bookingForm.value.Tel,
          Comment:this.bookingForm.value.Comment

        }
        this.service.BookCarNew(this.book).subscribe(data => {
          this.showBook=true;
          this.alert.showA({type:'success',message:'Время успешно забронированно.',show:true});
          
        },error => {
          console.clear();
          if(error.status==501){

            this.alert.showA({type:'wrong',message:'Время забронированно',show:true});
            this.submitted=false;
            df.value="";
            ds.value="";
            this.bookingForm.value.DateStart="";
            this.bookingForm.value.DateFinish="";
            
          }
          else if(error.status==502){
            this.alert.showA({type:'wrong',message:'Неверный пароль',show:true});
            this.submitted=false;
          }
          else if(error.status==503 || error.status==500){
            this.alert.showA({type:'wrong',message:'Некорректные данные',show:true});
            this.submitted=false;
          }
        }
        )
      }
    }
    hide(){
      this.service.showBookingForm=false;
    }
    showCarInfo(){
      

      this.service.showCarInfo=true;
    }
    getProgress(type:string, car:Car){
      if(car.Reports.length==0){
        
        this.rating[type]=0;
        return 0;
      }
      this.res=0;
      car.Reports.forEach(element => {
        this.res+=element[type];
      });
      this.res = this.res/car.Reports.length/5*100*2;
      this.rating[type]=this.res;
      return Math.round(this.res).toString()+'px';
    }
    ngOnInit() {
      if(localStorage.getItem("currentUser")){
        this.user=JSON.parse(localStorage.getItem("currentUser"));
      }
      this.service.car=null;
      this.bookingForm = this.formBuilder.group({
        Name: [this.user?this.user.Name:'', Validators.required],
        Email: [this.user?this.user.Email:'', Validators.required],
        Password: [this.user?'пароль':'', Validators.required],
        Tel: [''],
        DateStart:['', Validators.required],
        DateFinish:['', Validators.required],
        Place:['', Validators.required],
        Comment:['']
      });
      
      this.service.GetCar(this.route.snapshot.paramMap.get("id")).subscribe(data => {
       
        if(data){
        
          this.service.car=data;
          console.log(this.service.car);
          this.service.car.Reports.forEach(r => {
            r.CreatedDate=new Date(r.CreatedDate);
            r.ButtonText= "SHOW_COMMENTS";
          })
          this.service.car.Sales.forEach(r => {
            r.DateStart=new Date(r.DateStart);
            r.DateFinish=new Date(r.DateFinish);
          })
          this.sales = this.service.car.Sales.map(x =>{
            return {Discount:x.Discount, Id:x.Id, NewPrice:x.NewPrice, Checked:false, DaysNumber:x.DaysNumber}
          })
        }})
        
        
      
  }
  chooseSale(sale:any){
    if(!sale.Checked){
      this.sales.forEach(x => {x.Checked = false});
      sale.Checked = !sale.Checked;
      this.sale=sale;
      console.log(this.sale);
    }
    else{
      sale.Checked = !sale.Checked;
      this.sale.Id =0;
    }
    
    
    
  }

}
export interface Raiting{
  Look:number;
  Comfort:number;
  Drive:number;
}
export interface BookSale{
  SalesId:number;
}
