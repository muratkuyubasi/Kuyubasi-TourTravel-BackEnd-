import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonError } from '@core/error-handler/common-error';
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DepartureService {

  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService) { }

    updateDeparture(departure: any): Observable<any | CommonError> {
      const url = `departure/${departure.code}`;
      return this.httpClient.put<any>(url, departure)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    addDeparture(departure: any): Observable<any | CommonError> {
      const url = `departure`;
      return this.httpClient.post<any>(url, departure)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    deleteDeparture(id: string): Observable<void | CommonError> {
      const url = `departure/${id}`;
      return this.httpClient.delete<void>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    getDeparture(id: string): Observable<any | CommonError> {
      const url = `departure/${id}`;

      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getDepartureRecord(id: string): Observable<any | CommonError> {
      const url = `departure/GetDepartureRecord/${id}`;
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllDepartureByLang(languageCode: string): Observable<any | CommonError> {
      const url = `departure/GetAllDepartureByLang/${languageCode}`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
}
