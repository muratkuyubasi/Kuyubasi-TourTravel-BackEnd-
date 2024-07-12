import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "@environments/environment";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ContentService {
    path: string = environment.apiUrl;


    constructor(
        private httpClient: HttpClient) { }

    getList(): any {
        return this.httpClient.get<any>("FrontAnnouncement/GetFrontAnnouncementRecords");
    }
    delete(id: string):any{
        return this.httpClient.delete<any>("FrontAnnouncement/DeleteFrontAnnouncementRecord/"+id);
    }
    // approval(id:string):any {
    //     return this.httpClient.post<any>("TourComment/ConfirmTourComment?Id="+id);
    // }
    approval(id: string):any{
        return this.httpClient.post<any>("TourComment/ConfirmTourComment?Id="+id, null);
    }
   
    selecetPopular(id: string):any{
        return this.httpClient.post<any>("TourComment/SelectPopularTourComment?Id="+id, null);
    }
}