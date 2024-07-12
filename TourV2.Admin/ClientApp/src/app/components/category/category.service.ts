import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonError } from '@core/error-handler/common-error';
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService) { }

    updateCategory(category: any): Observable<any | CommonError> {
      const url = `category/${category.code}`;
      return this.httpClient.put<any>(url, category)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    addCategory(category: any): Observable<any | CommonError> {
      const url = `category`;
      return this.httpClient.post<any>(url, category)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    deleteCategory(id: string): Observable<void | CommonError> {
      const url = `category/${id}`;
      return this.httpClient.delete<void>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    getCategory(id: string): Observable<any | CommonError> {
      const url = `category/${id}`;

      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getCategoryRecord(id: string): Observable<any | CommonError> {
      const url = `category/GetCategoryRecord/${id}`;
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllCategoryByLang(languageCode: string): Observable<any | CommonError> {
      const url = `Category/GetAllCategoryByLang/${languageCode}`;
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
}
