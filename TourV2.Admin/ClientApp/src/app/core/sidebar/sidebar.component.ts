import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { UserAuth } from '@core/domain-classes/user-auth';
import { SecurityService } from '@core/security/security.service';
import { CommentService } from '@core/services/comment.service';
import { ContactService } from '@core/services/contact.service';
import { BaseComponent } from 'src/app/base.component';
declare var $: any;

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.scss'],
    animations: [
        trigger('slide', [
          state('up', style({ height: 0 })),
          state('down', style({ height: '*' })),
          transition('up <=> down', animate(200))
        ])
      ]
})
export class SidebarComponent extends BaseComponent implements OnInit {
  getcommentModel:any;
  getlistModel:any;

    appUserAuth: UserAuth = null;

  constructor(
    private contactService: ContactService, private commentService:CommentService, private securityService: SecurityService) {
    super();
  }

    ngOnInit() {
        this.setTopLogAndName();
        this.getcommentList();
        this.getcontactList();
      }
    
      setTopLogAndName() {
        this.sub$.sink = this.securityService.securityObject$
          .subscribe(c => {
            if (c) {
              this.appUserAuth = c;
            }
          })
      }

      getcommentList(){
        this.commentService.getList().subscribe(data=>{
         this.getcommentModel = data;
        //  console.log("Yorum liste",data)
        });
       }
       getcontactList() {
        this.contactService.getList().subscribe(data=>{
         this.getlistModel = data.data;
        //  console.log("İletişim liste",data)
        });
}
}