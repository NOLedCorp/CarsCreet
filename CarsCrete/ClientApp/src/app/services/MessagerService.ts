import { HttpClient } from '@angular/common/http';
import { Component, Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {User} from '../services/UserService';

export class MessagerService{
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    
    
    }
    saveMessage(mess:Message){
    
        return this.http.put<Message>(this.baseUrl + 'cars/save-message', mess);
    }
    createTopic(top:any){
    
        return this.http.put<Topic>(this.baseUrl + 'cars/create-topic', top);
    }
}

export interface Message{
    Id:number;
    CreateDate:Date;
    Text:string;
    UserId:number;
    TopicId:number;
}

export interface Topic{
    Id:number;
    UserId:number;
    UserReciverId:number;
    ModifyDate:Date;
    User:User;
    Messages:Message[];
}