import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CommonError } from '@core/error-handler/common-error';
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService) { }

    updateReservation(reservation: any): Observable<any | CommonError> {
      const url = `reservation/UpdateTourReservation`;
      return this.httpClient.put<any>(url, reservation)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    addReservation(reservation: any): Observable<any | CommonError> {
      console.log(reservation);
      const url = `reservation/AddTourReservation`;
      return this.httpClient.post<any>(url, reservation)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

  
    deleteReservation(id: string): Observable<void | CommonError> {
      const url = `reservation/DeleteTourReservation/${id}`;
      return this.httpClient.delete<void>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  
    getReservation(id: string): Observable<any | CommonError> {
      const url = `Reservation/GetReservation/${id}`;

      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getAllReservations(): Observable<any | CommonError> {
      const url = `reservation/GetAllTourReservation`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
    getAllDepartureByLang(languageCode: string): Observable<any | CommonError> {
      const url = `departure/GetAllDepartureByLang/${languageCode}`;
      
      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }


    addPerson(person:any): Observable<any | CommonError> {
      const url = `reservation/AddTourReservationPerson`;
      return this.httpClient.post<any>(url, person)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    updatePerson(person:any): Observable<any | CommonError> {
      const url = `reservation/UpdateTourReservationPerson`;
      return this.httpClient.put<any>(url, person)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    deletePerson(id:any): Observable<any | CommonError> {
      const url = `reservation/DeleteTourReservationPerson/${id}`;
      return this.httpClient.delete<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }


    getReservationTourId(id: string): Observable<any | CommonError> {
      const url = `Reservation/GetAllTourReservationTourId?id=${id}`;

      return this.httpClient.get<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

}
