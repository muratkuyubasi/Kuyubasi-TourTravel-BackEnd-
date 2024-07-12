import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonError } from '@core/error-handler/common-error';
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PeriodService {

  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService) { }

    updatePeriod(period: any): Observable<any | CommonError> {
      const url = `period/${period.code}`;
      return this.httpClient.put<any>(url, period)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    addPeriod(period: any): Observable<any | CommonError> {
      const url = `period`;
      return this.httpClient.post<any>(url, period)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    deletePeriod(id: string): Observable<void | CommonError> {
      const url = `period/${id}`;
      return this.httpClient.delete<void>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    getPeriod(id: string): Observable<any | CommonError> {
      const url = `period/${id}`;

      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getPeriodRecord(id: string): Observable<any | CommonError> {
      const url = `period/GetPeriodRecord/${id}`;
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllPeriodByLang(languageCode: string): Observable<any | CommonError> {
      const url = `period/GetAllPeriodByLang/${languageCode}`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
}
