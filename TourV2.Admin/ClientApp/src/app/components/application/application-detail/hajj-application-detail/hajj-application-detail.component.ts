import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Application } from '@core/domain-classes/application';
import { TranslationService } from '@core/services/translation.service';
import { ToastrService } from 'ngx-toastr';
import { ApplicationService } from '../../application.service';
import { BaseComponent } from 'src/app/base.component';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-hajj-application-detail',
  templateUrl: './hajj-application-detail.component.html',
  styleUrls: ['./hajj-application-detail.component.css']
})
export class HajjApplicationDetailComponent extends BaseComponent implements OnInit {
  isLoadingResults: boolean;
  candidate: Application;
  photoUrl: string = environment.apiUrl;
  pdfSrc: any;

  constructor(
    private activeRoute: ActivatedRoute,
    private toastrService: ToastrService,
    private translationService: TranslationService,
    private applicationService: ApplicationService,


  ) {
    super()
  }


  ngOnInit(): void {
    this.sub$.sink = this.activeRoute.params.subscribe((params: { id: number }) => {
      const id = params.id;
      if (id) {
        this.getHajjCandidate(id);
      }
      this.sub$.sink = this.applicationService.getHajjMemberCardPdf(id).subscribe((data: any) => {
        this.pdfSrc = environment.apiUrl + data;
        let iframe = document.createElement('iframe');
        iframe.src = this.pdfSrc;
        iframe.width = '100%';
        iframe.height = '530px';
        document.body.appendChild(iframe);
        let container = document.querySelector('.application-card-pdf');
        container.appendChild(iframe);
      },
        (error: any) => {
          console.error(error);
        });
    });
  }


  getHajjCandidate(id: number) {
    this.sub$.sink = this.applicationService.getHajjCandidatesById(id).subscribe(
      (data: any) => {
        this.candidate = data;
      },
      (error) => {
        this.handleHttpError(error);
      }
    );
  }



  handleHttpError(error: any): void {
    if (error && error.status === 409) {

      this.toastrService.error(this.translationService.getValue('Aynı kayıttan bulunmakta.'));
    } else {

      this.toastrService.error(this.translationService.getValue('Bilinmeyen Hata'));
    }
  }

}
