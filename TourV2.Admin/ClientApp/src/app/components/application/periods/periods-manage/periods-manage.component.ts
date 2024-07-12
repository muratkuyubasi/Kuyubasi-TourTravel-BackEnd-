import { Component, Inject, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ApplicationService } from '../../application.service';
import { BaseComponent } from 'src/app/base.component';
import { UntypedFormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-periods-manage',
  templateUrl: './periods-manage.component.html',
  styleUrls: ['./periods-manage.component.css']
})
export class PeriodsManageComponent extends BaseComponent implements OnInit {
  isEditMode: boolean = false;
  isLoadingButton: boolean = false;
  form: UntypedFormGroup


  constructor(
    public dialogRef: MatDialogRef<PeriodsManageComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private applicationService: ApplicationService,
    private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    private translationService: TranslationService
  ) {
    super();
  }

  ngOnInit() {
    this.createForm();
    if (this.data.periods) {
      this.form.patchValue(this.data.periods)
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  createForm() {
    this.form = this.formBuilder.group({
      id: [''],
      name: ['', Validators.required],
      isActive: [true]
    })

  }


  save(): void {
    this.isLoadingButton = true;
    if (this.data.periods) {
      if (this.data.type == 1) {
        this.sub$.sink = this.applicationService.updateHajjPeriods(this.form.value).subscribe((data: any) => {
          this.isLoadingButton = false;
          this.toastrService.success(this.translationService.getValue('UPDATED_SUCCESSFULLY'));
          this.dialogRef.close();
        })

      }
      else {
        this.sub$.sink = this.applicationService.updateUmrahPeriods(this.form.value).subscribe((data: any) => {
          this.isLoadingButton = false;
          this.toastrService.success(this.translationService.getValue('UPDATED_SUCCESSFULLY'));
          this.dialogRef.close();

        })
      }

    } else {
      if (this.data.type == 1) {
        this.sub$.sink = this.applicationService.addHajjPeriods(this.form.value).subscribe((data: any) => {
          this.isLoadingButton = false;
          this.toastrService.success(this.translationService.getValue('SAVED_SUCCESSFULLY'));
          this.dialogRef.close();

        })

      }
      else {
        this.sub$.sink = this.applicationService.addUmrahPeriods(this.form.value).subscribe((data: any) => {
          this.isLoadingButton = false;
          this.toastrService.success(this.translationService.getValue('SAVED_SUCCESSFULLY'));
          this.dialogRef.close();

        })
      }

    }


  }
}
