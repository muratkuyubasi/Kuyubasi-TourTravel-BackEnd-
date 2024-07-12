import { Injectable } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';

@Injectable({
  providedIn: 'root',
})
export class PaginatorService {
  constructor(private matPaginatorIntl: MatPaginatorIntl) {}

  configurePaginator(): void {
    this.matPaginatorIntl.itemsPerPageLabel = 'Sayfadaki veri sayısı:';
    this.matPaginatorIntl.lastPageLabel = 'Son sayfa';
    this.matPaginatorIntl.firstPageLabel = 'İlk sayfa';
    this.matPaginatorIntl.nextPageLabel = 'Sonraki sayfa';
    this.matPaginatorIntl.previousPageLabel = 'Önceki sayfa';
  }
}