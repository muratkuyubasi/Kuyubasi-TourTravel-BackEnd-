import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "@environments/environment";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class SubscriptionService {
    path: string = environment.apiUrl;


    constructor(
        private httpClient: HttpClient) { }

    getList(): any {
        return this.httpClient.get<any>("/Contact/GetAllNewsletterSubscriptions");
    }
}