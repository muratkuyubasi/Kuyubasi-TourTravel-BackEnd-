import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from '@core/domain-classes/user';
import { TranslationService } from '@core/services/translation.service';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/base.component';
import { ActiveTourService } from '../activetour.service';

@Component({
  selector: 'app-update-specification',
  templateUrl: './update-specification.component.html',
})
export class UpdateSpecificationComponent extends BaseComponent implements OnInit {
  
  updateForm: UntypedFormGroup;
  constructor(
    private activeTourService: ActiveTourService,
    private fb: UntypedFormBuilder,
    public dialogRef: MatDialogRef<UpdateSpecificationComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private toastrService: ToastrService,
    private translationService: TranslationService) {
    super();
  }

  ngOnInit(): void {
    this.createUpdateForm();
    this.updateForm.patchValue(this.data);
  }

  createUpdateForm() {
    this.updateForm = this.fb.group({
      id: [],
      specification:[],
      inPrice:[]
    });
  }


  saveData() {
    if (this.updateForm.valid) {
      this.sub$.sink = this.activeTourService.updateSpec(this.createBuildObject()).subscribe(d => {
        this.toastrService.success(this.translationService.getValue('SUCCESSFULL_RPRICE_UPDATE'))
        this.dialogRef.close(this.createBuildObject());
      })
    }
  }

  createBuildObject(): any {
    return {
      id: this.data.id,
      specification: this.updateForm.get('specification').value,
      inPrice: this.updateForm.get('inPrice').value
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
