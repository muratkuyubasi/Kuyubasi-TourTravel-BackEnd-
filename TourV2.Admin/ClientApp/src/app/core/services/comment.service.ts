import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "@environments/environment";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class CommentService {
    path: string = environment.apiUrl;


    constructor(
        private httpClient: HttpClient) { }

    getList(): any {
        return this.httpClient.get<any>("TourComment/GetAllTourComments");
    }
    getid(id:string):Observable<any> {
        return this.httpClient.get<any>("TourComment/GetTourCommentByMainId?activeTourId"+id);
    }
    // approval(id:string):any {
    //     return this.httpClient.post<any>("TourComment/ConfirmTourComment?Id="+id);
    // }
    approval(id: string):any{
        return this.httpClient.post<any>("TourComment/ConfirmTourComment?Id="+id, null);
    }
    delete(id: string):any{
        return this.httpClient.delete<any>("TourComment/DeleteTourComment?Id="+id);
    }
    selecetPopular(id: string):any{
        return this.httpClient.post<any>("TourComment/SelectPopularTourComment?Id="+id, null);
    }
}