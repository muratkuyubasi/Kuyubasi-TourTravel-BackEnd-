import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "@environments/environment";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class EducationService {
    path: string = environment.apiUrl;


    constructor(
        private httpClient: HttpClient) { }

    getList(): any {
        return this.httpClient.get<any>("Education/GetAllEducationForms");
    }
    getid(id:string):Observable<any> {
        return this.httpClient.get<any>("TourComment/GetTourCommentByMainId?activeTourId"+id);
 
}
}