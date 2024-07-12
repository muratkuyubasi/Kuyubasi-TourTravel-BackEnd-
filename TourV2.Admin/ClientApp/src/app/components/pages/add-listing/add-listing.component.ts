import { Component, OnInit } from '@angular/core';
import { ContactService } from '@core/services/contact.service';
import { ContentService } from '@core/services/content.service';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-add-listing',
  templateUrl: './add-listing.component.html',
  styleUrls: ['./add-listing.component.scss']
})
export class AddListingComponent implements OnInit {

  constructor(private contentService:ContentService) { }
  path = environment.serverUrl

  getContentModel!:any;
  getdelete:any;

  ngOnInit(): void {
    this.getcontentList();
  }

  getcontentList(){
    this.contentService.getList().subscribe(data=>{
     this.getContentModel = data;
     console.log("Slider",data)
    });
   }

   changeDelete(id:any){
    this.contentService.delete(id).subscribe(data=> {
      this.getdelete = data;
    })
    
  }
}
