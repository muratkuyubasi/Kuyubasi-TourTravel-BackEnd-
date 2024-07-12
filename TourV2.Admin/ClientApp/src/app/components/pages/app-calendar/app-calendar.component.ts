import { Component, OnInit } from '@angular/core';
import { RequestService } from '@core/services/request.service';

@Component({
  selector: 'app-app-calendar',
  templateUrl: './app-calendar.component.html',
  styleUrls: ['./app-calendar.component.scss']
})
export class AppCalendarComponent implements OnInit {

  getrequestModel!:any;

  constructor(private requestService:RequestService) { }

  ngOnInit(): void {
    this.getrequestList();
  }

  getrequestList(){
    this.requestService.getList().subscribe(data=>{
     this.getrequestModel = data;
     console.log("Tur Talebi liste",data)
    });
   }

}
