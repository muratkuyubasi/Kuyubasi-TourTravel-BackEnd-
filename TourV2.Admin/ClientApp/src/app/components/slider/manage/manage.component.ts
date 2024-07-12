import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommentService } from '@core/services/comment.service';
import { CommonService } from '@core/services/common.service';
import { SliderService } from '../slider.service';

@Component({
  selector: 'app-reviews',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.scss']
})
export class ReviewsComponent implements OnInit {

  getContentModel!:any
  getdelete:any;

  constructor(private sliderSerivce:SliderService, private router: ActivatedRoute) { }

  ngOnInit(): void {
    this.getSliderList()
  }

  getSliderList(){
      this.sliderSerivce.getList().subscribe(data=>{
       this.getContentModel = data;
       console.log("Slider",data)
      });
     }


    // changeDelete(id:any){
    //   this.sliderSerivce.delete(id).subscribe(data=> {
    //     this.getdelete = data;
    //     this.getcommentList()
    //   })
      
    // }
     }

  

  



