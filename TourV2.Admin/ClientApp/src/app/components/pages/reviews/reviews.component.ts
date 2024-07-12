import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommentService } from '@core/services/comment.service';
import { CommonService } from '@core/services/common.service';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.scss']
})
export class ReviewsComponent implements OnInit {

  getcommentModel:any;
  getconfirmationModel:any;
  getPopularModel:any;
  getdelete:any;

  constructor(private commentService:CommentService, private router: ActivatedRoute) { }

  ngOnInit(): void {
    this.getcommentList()
  }

  getcommentList(){
      this.commentService.getList().subscribe(data=>{
       this.getcommentModel = data;
      //  console.log("Yorum liste",data)
      });
     }
     commentConfirmation (id:any) {
      this.commentService.approval(id).subscribe(data=> {
        this.getconfirmationModel = data;
        // console.log("Yorum onay", this.getconfirmationModel)
      })
    }
    changeActive(id:any){
      this.commentService.approval(id).subscribe(data=> {
        this.getconfirmationModel = data;
        this.getcommentList()
      })
    }
    changePopular(id:any){
      this.commentService.selecetPopular(id).subscribe(data=> {
        this.getPopularModel = data;
        // console.log("adasda",data)
        this.getcommentList()
      })
    }

    changeDelete(id:any){
      this.commentService.delete(id).subscribe(data=> {
        this.getdelete = data;
        this.getcommentList()
      })
      
    }
     }

  

  



