import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonError } from '@core/error-handler/common-error';
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ActiveTourService {

  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService) { }

    updateActiveTour(activetour: any): Observable<any | CommonError> {
      const url = `activetour/UpdateTour/${activetour.code}`;
      return this.httpClient.put<any>(url, activetour)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    addActiveTour(activetour: any): Observable<any | CommonError> {
      const url = `activetour/AddTour`;
      return this.httpClient.post<any>(url, activetour)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    deleteActiveTour(id: string): Observable<void | CommonError> {
      const url = `activetour/${id}`;
      return this.httpClient.delete<void>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    getActiveTour(id: string): Observable<any | CommonError> {
      const url = `ActiveTour/GetActiveTour/${id}`;

      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getActiveTourRecord(id: string): Observable<any | CommonError> {
      const url = `activetour/GetActiveTourRecord/${id}`;
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllActiveTourByLang(languageCode: string): Observable<any | CommonError> {
      const url = `activetour/GetAllTourByLang/${languageCode}`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllTourByLang(languageCode: string): Observable<any | CommonError> {
      const url = `tour/GetAllTourByLang/${languageCode}`;
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllCategoryByLang(languageCode: string): Observable<any | CommonError> {
      const url = `Category/GetAllCategoryByLang/${languageCode}`;
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
    getAllPeriodByLang(languageCode: string): Observable<any | CommonError> {
      const url = `period/GetAllPeriodByLang/${languageCode}`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
    getAllRegionByLang(languageCode: string): Observable<any | CommonError> {
      const url = `region/GetAllRegionByLang/${languageCode}`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllDepartureByLang(languageCode: string): Observable<any | CommonError> {
      const url = `departure/GetAllDepartureByLang/${languageCode}`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllTourDepartures(id: any): Observable<any | CommonError> {
      const url = `activeTour/GetAllTourDepartureByLang/${id}`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }


    addPrice(price:any): Observable<any | CommonError> {
      const url = `activetour/AddPrice`;
      return this.httpClient.post<any>(url, price)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    updatePrice(price:any): Observable<any | CommonError> {
      const url = `activetour/UpdatePrice`;
      return this.httpClient.put<any>(url, price)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    deletePrice(price:any): Observable<any | CommonError> {
      const url = `activetour/DeletePrice/${price.id}`;
      return this.httpClient.delete<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    addDay(day:any): Observable<any | CommonError> {
      
      const url = `activetour/AddDay`;
      return this.httpClient.post<any>(url, day)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    updateDay(item:any): Observable<any | CommonError> {
      const url = `activetour/UpdateDay`;
      return this.httpClient.put<any>(url, item)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    deleteDay(item:any): Observable<any | CommonError> {
      const url = `activetour/DeleteDay/${item.id}`;
      return this.httpClient.delete<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }


    addSpec(spec:any): Observable<any | CommonError> {
      
      const url = `activetour/AddSpecification`;
      return this.httpClient.post<any>(url, spec)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    updateSpec(item:any): Observable<any | CommonError> {
      const url = `activetour/UpdateSpecification`;
      return this.httpClient.put<any>(url, item)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    deleteSpec(item:any): Observable<any | CommonError> {
      const url = `activetour/DeleteSpecification/${item.id}`;
      return this.httpClient.delete<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }


    addDeparture(departure:any): Observable<any | CommonError> {
      const url = `activetour/AddDeparture`;
      return this.httpClient.post<any>(url, departure)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    updateDeparture(item:any): Observable<any | CommonError> {
      const url = `activetour/UpdateDeparture`;
      return this.httpClient.put<any>(url, item)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    deleteDeparture(item:any): Observable<any | CommonError> {
      const url = `activetour/DeleteDeparture/${item.id}`;
      return this.httpClient.delete<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
    
    addMedia(media:FormData): Observable<any | CommonError> {
      console.log(media)
      const url = `activetour/AddMedia`;
      return this.httpClient.post<any>(url, media)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    deleteMedia(item:any): Observable<any | CommonError> {
      const url = `activetour/DeleteMedia/${item.id}`;
      return this.httpClient.post<any>(url,null)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
}
