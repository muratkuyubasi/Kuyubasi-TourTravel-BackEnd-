import { Component } from '@angular/core';
import { Periods } from '@core/domain-classes/application';
import { BaseComponent } from 'src/app/base.component';
import { ApplicationService } from '../../application.service';
import { MatDialog } from '@angular/material/dialog';
import { PeriodsManageComponent } from '../periods-manage/periods-manage.component';

@Component({
  selector: 'app-umrah-periods-list',
  templateUrl: './umrah-periods-list.component.html',
  styleUrls: ['./umrah-periods-list.component.css']
})
export class UmrahPeriodsListComponent extends BaseComponent {
  isLoadingResults = false;
  umrahPeriods: Periods[]=[];


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
    this.sub$.sink = this.applicationService.getUmrahPeriods().subscribe((data: any) => {
      this.umrahPeriods = data
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
