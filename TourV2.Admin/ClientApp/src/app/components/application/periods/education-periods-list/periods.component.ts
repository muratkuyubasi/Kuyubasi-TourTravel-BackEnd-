import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Application, Periods } from '@core/domain-classes/application';
import { ApplicationService } from '../../application.service';
import { BaseComponent } from 'src/app/base.component';
import { MatDialog } from '@angular/material/dialog';
import { PeriodsManageComponent } from '../periods-manage/periods-manage.component';

@Component({
  selector: 'app-periods',
  templateUrl: './periods.component.html',
  styleUrls: ['./periods.component.css']
})
export class PeriodsComponent extends BaseComponent {
  isLoadingResults = false;
  hajjPeriods: any;


  constructor(
    private applicationService: ApplicationService,
    private dialog: MatDialog
  ) {
    super();
  }

  ngOnInit() {
    this.getAll();
  }



  getAll() {
    this.sub$.sink = this.applicationService.getHajjPeriods().subscribe((data: any) => {
      this.hajjPeriods = data
      console.log("Dönemler",this.hajjPeriods.data)

    })
  }

  managePeriod(type?, periods?): void {
    const dialogRef = this.dialog.open(PeriodsManageComponent, {
      width: '300px',
      data: { type, periods }
    });
    dialogRef.afterClosed().subscribe(result => {
      this.getAll();

    })

  }

}
