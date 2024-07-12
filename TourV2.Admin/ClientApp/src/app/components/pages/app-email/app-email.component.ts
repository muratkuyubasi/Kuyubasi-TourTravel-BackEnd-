import { Component, OnInit } from '@angular/core';
import { ContactService } from '@core/services/contact.service';

@Component({
  selector: 'app-app-email',
  templateUrl: './app-email.component.html',
  styleUrls: ['./app-email.component.scss']
})
export class AppEmailComponent implements OnInit {

  constructor(private contactService: ContactService) { }
  getlistModel!:any;


  ngOnInit(): void {
    this.getcontactList();
  }

  getcontactList() {
    this.contactService.getList().subscribe(data=>{
     this.getlistModel = data.data;
     console.log("İletişim liste",data)
    });
  
   }

}
