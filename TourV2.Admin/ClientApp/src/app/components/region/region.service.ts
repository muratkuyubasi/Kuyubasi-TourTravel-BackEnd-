import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonError } from '@core/error-handler/common-error';
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RegionService {

  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService) { }

    updateRegion(region: any): Observable<any | CommonError> {
      const url = `region/${region.code}`;
      return this.httpClient.put<any>(url, region)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    addRegion(region: any): Observable<any | CommonError> {
      const url = `region`;
      return this.httpClient.post<any>(url, region)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    deleteRegion(id: string): Observable<void | CommonError> {
      const url = `region/${id}`;
      return this.httpClient.delete<void>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    getRegion(id: string): Observable<any | CommonError> {
      const url = `region/${id}`;

      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getRegionRecord(id: string): Observable<any | CommonError> {
      const url = `region/GetRegionRecord/${id}`;
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllRegionByLang(languageCode: string): Observable<any | CommonError> {
      const url = `region/GetAllRegionByLang/${languageCode}`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
}
