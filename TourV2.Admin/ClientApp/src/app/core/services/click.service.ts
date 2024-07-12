import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "@environments/environment";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ClickService {
    path: string = environment.apiUrl;


    constructor(
        private httpClient: HttpClient) { }

    getid(id:number):Observable<any> {
        return this.httpClient.get<any>("Tour/GetTourClickCount?activeTourId="+id);

    }
}