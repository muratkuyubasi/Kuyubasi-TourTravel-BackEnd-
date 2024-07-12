import { environment } from './../../../../../environments/environment';
import { LoadingIndicatorComponent } from './../../../../shared/loading-indicator/loading-indicator.component';
import { Component, EnvironmentInjector, OnInit } from '@angular/core';
import { FormBuilder, UntypedFormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CommonError } from '@core/error-handler/common-error';
import { TranslationService } from '@core/services/translation.service';
import moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/base.component';
import { ApplicationService } from '../../application.service';
import { Application } from '@core/domain-classes/application';

@Component({
  selector: 'app-umrah-application-detail',
  templateUrl: './umrah-application-detail.component.html',
  styleUrls: ['./umrah-application-detail.component.css']
})
export class UmrahApplicationDetailComponent extends BaseComponent implements OnInit {
  detailForm: UntypedFormGroup;
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
        this.getUmrahCandidate(id);
        this.sub$.sink = this.applicationService.getUmrahMemberCardPdf(id).subscribe((data: any) => {
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
      }
    });
    this.detailForm.disable();

  }


  getUmrahCandidate(id: number) {
    this.sub$.sink = this.applicationService.getUmrahCandidatesById(id).subscribe(
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
