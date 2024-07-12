import { Injectable } from '@angular/core';
import { CommonError } from '@core/error-handler/common-error';
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Application, Periods } from '@core/domain-classes/application';


@Injectable({
  providedIn: 'root'
})
export class ApplicationService {

  constructor(private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService) { }

    // ~~~~~~~~~~~~~~~~~~Umrah~~~~~~~~~~~~~~~~~~~~~ //
    getUmrahCandidates(skip,pageSize,search,periodId): Observable<Application | CommonError> {
      const url = "UmreForm/GetAllUmrahCandidates/"+skip+"/"+pageSize+"?search="+search+"&periodId="+periodId;
      return this.httpClient.get<Application>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    getUmrahCandidatesById(id: number): Observable<Application | CommonError> {
      const url = `UmreForm/GetUmrahCandidateById?id=${id}`;
      return this.httpClient.get<Application>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    updateUmrahCandidate(candidate:any): Observable<any | CommonError> {
      const url = `UmreForm/UpdateCandidateById`;
      return this.httpClient.put<any>(url, candidate)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }


    deleteUmrahCandidate(id: number): Observable<any | CommonError> {
      const url = `UmreForm/DeleteById?id=${id}`;
      return this.httpClient.delete<any>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
  
    }

    getUmrahMemberCardPdf(id): Observable<Application | CommonError> {
      const url = `UmreForm/GetUmrahMemberCardPDF?id=${id}`;
      return this.httpClient.get<Application>(url)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }


   
  

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ //

  addFile(data:any): Observable<any | CommonError>{
    const url=`UmreForm/PostPicture`
    return this.httpClient.post<any>(url,data)
    .pipe(catchError(this.commonHttpErrorService.handleError));
  }


  getAllAssociations(): Observable<Application | CommonError>{
    const url = `Association/GetAllAssociations`;
    return this.httpClient.get<Application>(url)
    .pipe(catchError(this.commonHttpErrorService.handleError));
  }

  getAllAirports(): Observable<Application | CommonError>{
    const url = `Airport/GetAllAirports`;
    return this.httpClient.get<Application>(url)
    .pipe(catchError(this.commonHttpErrorService.handleError));
  }

  getAllRoomTypes(): Observable<Application | CommonError>{
    const url = `RoomType/GetAllRoomTypes`;
    return this.httpClient.get<Application>(url)
    .pipe(catchError(this.commonHttpErrorService.handleError));
  }












    // ~~~~~~~~~~~~~~~~~Hajj-Periods~~~~~~~~~~~~~~~~~~ //

    addHajjPeriods(data:any): Observable<Periods | CommonError>{
      const url=`Education/AddPeriodEducation`
      return this.httpClient.post<Periods>(url,data)
      .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    
    getHajjPeriods(): Observable<Periods | CommonError>{
      const url = `Education/GetAllPeriodEducation`;
      return this.httpClient.get<Periods>(url)
      .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    updateHajjPeriods(data:any): Observable<Periods | CommonError> {
      const url = `Education/UpdateHP`;
      return this.httpClient.put<Periods>(url, data)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
    
    // ~~~~~~~~~~~~~~~~~Umrah-Periods~~~~~~~~~~~~~~~~~~ //

    addUmrahPeriods(data:any): Observable<Periods | CommonError>{
      const url=`Period/AddUP`
      return this.httpClient.post<Periods>(url,data)
      .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    
    getUmrahPeriods(): Observable<Periods | CommonError>{
      const url = `Period/GetListUP`;
      return this.httpClient.get<Periods>(url)
      .pipe(catchError(this.commonHttpErrorService.handleError));
    }

    updateUmrahPeriods(data:any): Observable<Periods | CommonError> {
      const url = `Period/UpdateUP`;
      return this.httpClient.put<Periods>(url, data)
        .pipe(catchError(this.commonHttpErrorService.handleError));
    }
  




















     // ~~~~~~~~~~~~~~~~~~Hajj~~~~~~~~~~~~~~~~~~~~~ //


  getHajjCandidates(periodId): Observable<Application | CommonError> {
    const url = "Education/GetAllEducationFormByPeriodId/"+periodId;
    return this.httpClient.get<any>(url)
      .pipe(catchError(this.commonHttpErrorService.handleError));
  }


  getHajjCandidatesById(id: number): Observable<Application | CommonError> {
    const url = `Education/GetPeriodEducationById?id=${id}`;
    return this.httpClient.get<Application>(url)
      .pipe(catchError(this.commonHttpErrorService.handleError));
  }


  updateHajjCandidate(candidate:any): Observable<any | CommonError> {
    const url = `HacForm/UpdateCandidateById`;
    return this.httpClient.put<any>(url, candidate)
      .pipe(catchError(this.commonHttpErrorService.handleError));
  }

  deleteHajjCandidate(id: number): Observable<any | CommonError> {
    const url = `HacForm/DeleteById?id=${id}`;
    return this.httpClient.delete<any>(url)
      .pipe(catchError(this.commonHttpErrorService.handleError));

  }

  getHajjMemberCardPdf(id): Observable<Application | CommonError> {
    const url = `HacForm/GetHACMemberCardPDF?id=${id}`;
    return this.httpClient.get<Application>(url)
      .pipe(catchError(this.commonHttpErrorService.handleError));
  }
}
