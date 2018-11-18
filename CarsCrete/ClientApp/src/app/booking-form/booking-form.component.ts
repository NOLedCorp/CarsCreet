import { Component, OnInit, OnChanges, SimpleChange, SimpleChanges, Input } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CarsService, Car, Book } from '../services/CarsService';
import { AlertService } from '../services/AlertService';
import {User, ShowSale} from '../services/UserService';
import { ActivatedRoute } from "@angular/router";
import {TranslateService} from '@ngx-translate/core';
import {Subscription} from 'rxjs';

@Component({
  selector: 'booking-form',
  templateUrl: './booking-form.component.html',
  styleUrls: ['./booking-form.component.css']
})
export class BookingFormComponent implements OnInit{
  showBook:boolean = false;
  bookingForm: FormGroup;
  public sales:ShowSale[];
  submitted = false;sale:ShowSale = new ShowSale();
  salesError:boolean = false;
  res:number=0;
  rating:Raiting = {Look:0, Comfort:0, Drive:0};
  public book:Book;
  public user:User;
  private subscription: Subscription;
  
  
  
  constructor(public translate: TranslateService,private formBuilder: FormBuilder,private route: ActivatedRoute, public service:CarsService, public alert:AlertService) { 
    this.sale.Id = 0;
  }
  get f() { return this.bookingForm.controls; }
  Round(k:number){
    let res = Math.round(k*100)/100;
    
    return res.toFixed(2)
  }
    onSubmit(ds:HTMLInputElement, df:HTMLInputElement) {
      this.submitted=true;
      if (this.bookingForm.invalid) {
        return
        
      }
      if(!this.checkSale()){
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
        this.service.BookCar(this.book).subscribe(data => {
          this.bookingForm.reset();
          this.submitted = false;
          this.clearSales();
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
          this.clearSales();
          this.bookingForm.reset();
          this.submitted = false;
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
        Tel: [this.user?(this.user.Phone?this.user.Phone:''):''],
        DateStart:['', Validators.required],
        DateFinish:['', Validators.required],
        Place:['', Validators.required],
        Comment:['']
      });
      
      this.service.GetCar(this.route.snapshot.paramMap.get("id")).subscribe(data => {
       
        if(data){
        
          this.service.car=data;
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
          this.route.queryParamMap.subscribe(data => this.chooseNewSale(Number(data.get('saleId'))));
         
        }})
        
    this.bookingForm.valueChanges.subscribe(data => {
      this.checkSale();
    })
      
  }
  chooseSale(sale:any){
    if(!sale.Checked){
      this.clearSales();
      sale.Checked = !sale.Checked;
      this.sale=sale;
      
      
    }
    else{
      sale.Checked = !sale.Checked;
      this.sale=null;
      if(this.submitted){
        this.salesError = !this.checkSale();
      }
    }
    if(this.submitted){
      this.salesError = !this.checkSale();
    }
    
    
    
  }
  chooseNewSale(id:number){
    
    this.sale=this.sales.filter(x => x.Id == id)[0];
    this.sale.Checked=true;
  }
  checkSale(){
    if(this.sale && this.sale.Id!=0 && this.sale.DaysNumber!=0){
      if((new Date(this.bookingForm.value.DateFinish).getTime()-new Date(this.bookingForm.value.DateStart).getTime())/86400000<this.sale.DaysNumber){
        this.salesError = true;
        return false
      }
      else{
        this.salesError = false;
        return true
      }
    }
    else{
      return true
    }
  }
  clearSales(){
    this.sales.forEach(x => {x.Checked = false});
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
