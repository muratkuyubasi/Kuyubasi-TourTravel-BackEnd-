import { Component, OnInit } from '@angular/core';
import { SubscriptionService } from '@core/services/subscription.service';

@Component({
  selector: 'app-app-todo',
  templateUrl: './app-todo.component.html',
  styleUrls: ['./app-todo.component.scss']
})
export class AppTodoComponent implements OnInit {

  constructor( private subscriptionService: SubscriptionService) { }
    getlistModel!: any;


  ngOnInit(): void {
    this.getList();


  }
   getList() {
    this.subscriptionService.getList().subscribe(data=>{
     this.getlistModel = data.data;
     console.log(data)
    });
  
   }

}
