import { Component, OnInit } from '@angular/core';
import { EducationService } from '@core/services/education.service';
import * as XLSX from 'xlsx';
@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss']
})
export class InvoiceComponent implements OnInit {

  educationModel!:any
  fileName:string = "Değerler Eğitimi Formu.xlsx";

  constructor(private educationService:EducationService) { }

  ngOnInit(): void {
    
    this.educationList();
  }

  exportExcel(): void {
    let element = document.getElementById('excel-table');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'ana liste');

    XLSX.writeFile(wb, this.fileName);
  }

  educationList(){
    this.educationService.getList().subscribe(data=>{
     this.educationModel = data;
     console.log("Form",data)
    });
   }

   

}
