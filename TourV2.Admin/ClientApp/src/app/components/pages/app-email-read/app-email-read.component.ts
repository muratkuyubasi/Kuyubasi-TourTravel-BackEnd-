import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactService } from '@core/services/contact.service';

@Component({
  selector: 'app-app-email-read',
  templateUrl: './app-email-read.component.html',
  styleUrls: ['./app-email-read.component.scss']
})
export class AppEmailReadComponent implements OnInit {

  constructor(private contactService: ContactService, private router: ActivatedRoute, private route: Router) { }
  getlistModel!:any;


  ngOnInit(): void {
    this.router.params.subscribe(x=>{
      this.getContactid(x['id']);

    })
  }
  getContactid(id:any){
    this.contactService.getid(id).subscribe(data=> {
      this.getlistModel = data;
      
      console.log("İletişim detay kişi", this.getlistModel)
    })
  }
}
