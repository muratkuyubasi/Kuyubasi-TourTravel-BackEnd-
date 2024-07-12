import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonError } from '@core/error-handler/common-error';
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TourService {

  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService) { }

    updateTour(tour: any): Observable<any | CommonError> {
      const url = `tour/${tour.code}`;
      return this.httpClient.put<any>(url, tour)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    addTour(tour: any): Observable<any | CommonError> {
      const url = `tour`;
      return this.httpClient.post<any>(url, tour)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    deleteTour(id: string): Observable<void | CommonError> {
      const url = `tour/${id}`;
      return this.httpClient.delete<void>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    getTour(id: string): Observable<any | CommonError> {
      const url = `tour/${id}`;

      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getTourRecord(id: string): Observable<any | CommonError> {
      const url = `tour/GetTourRecord/${id}`;
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllTourByLang(languageCode: string): Observable<any | CommonError> {
      const url = `tour/GetAllTourByLang/${languageCode}`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
}
