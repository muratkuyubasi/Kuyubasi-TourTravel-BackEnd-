import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from '@core/domain-classes/user';
import { TranslationService } from '@core/services/translation.service';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/base.component';
import { ActiveTourService } from '../activetour.service';

@Component({
  selector: 'app-update-price',
  templateUrl: './update-price.component.html',
  styleUrls: ['./update-price.component.scss']
})
export class UpdatePriceComponent extends BaseComponent implements OnInit {
  
  updateForm: UntypedFormGroup;
  constructor(
    private activeTourService: ActiveTourService,
    private fb: UntypedFormBuilder,
    public dialogRef: MatDialogRef<UpdatePriceComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private toastrService: ToastrService,
    private translationService: TranslationService) {
    super();
  }

  ngOnInit(): void {
    this.createUpdateForm();
    this.updateForm.patchValue(this.data);
    // this.updatePriceForm.get('title').setValue(this.data.title);
  }

  createUpdateForm() {
    this.updateForm = this.fb.group({
      id: [],
      title:[],
      price:[],
      currency:[],
      extraPrice:[],
      discount:[],
      isChildPrice:[]
    });
  }


  savePrice() {
    if (this.updateForm.valid) {
      this.sub$.sink = this.activeTourService.updatePrice(this.createBuildObject()).subscribe(d => {
        this.toastrService.success(this.translationService.getValue('SUCCESSFULL_RPRICE_UPDATE'))
        this.dialogRef.close(this.createBuildObject());
      })
    }
  }

  createBuildObject(): any {
    return {
      id: this.data.id,
      title: this.updateForm.get('title').value,
      price: this.updateForm.get('price').value,
      currency: this.updateForm.get('currency').value,
      extraPrice: this.updateForm.get('extraPrice').value,
      discount: this.updateForm.get('discount').value,
      isChildPrice: this.data.isChildPrice
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
