import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from '@core/domain-classes/user';
import { TranslationService } from '@core/services/translation.service';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/base.component';
import { ActiveTourService } from '../activetour.service';
import * as moment from 'moment'

@Component({
  selector: 'app-update-departure',
  templateUrl: './update-departure.component.html',
})
export class UpdateDepartureComponent extends BaseComponent implements OnInit {
  
  departures:any[]=[]
  updateForm: UntypedFormGroup;
  constructor(
    private activeTourService: ActiveTourService,
    private fb: UntypedFormBuilder,
    public dialogRef: MatDialogRef<UpdateDepartureComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private toastrService: ToastrService,
    private translationService: TranslationService) {
    super();
  }

  ngOnInit(): void {
    this.activeTourService.getAllDepartureByLang("tr").subscribe((resp:any)=>{
      this.departures=resp
    })
    this.createUpdateForm();
    this.data.departureTime = moment(this.data.departureTime).format("yyyy-MM-DD HH:mm")
    this.updateForm.patchValue(this.data);
  }

  createUpdateForm() {
    this.updateForm = this.fb.group({
      id: [],
      departureTime:[],
      departureRecordId:[],
      isMain:[]
    });
  }


  saveData() {
    if (this.updateForm.valid) {
      this.sub$.sink = this.activeTourService.updateDeparture(this.createBuildObject()).subscribe(d => {
        this.toastrService.success(this.translationService.getValue('SUCCESSFULL_UPDATE'))
        this.dialogRef.close(this.createBuildObject());
      })
    }
  }

  createBuildObject(): any {
    return {
      id: this.data.id,
      departureTime: this.updateForm.get('departureTime').value,
      departureRecordId: this.updateForm.get('departureRecordId').value,
      isMain: this.updateForm.get('isMain').value,
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
