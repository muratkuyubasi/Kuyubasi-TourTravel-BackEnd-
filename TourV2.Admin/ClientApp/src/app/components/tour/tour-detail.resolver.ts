import { Injectable } from '@angular/core';
import {
    Resolve,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';

import { Observable } from 'rxjs';
import { TourService } from './tour.service';

@Injectable()
export class TourDetailResolverService implements Resolve<any> {
    constructor(private tourService: TourService) { }
    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> {
        const name = route.paramMap.get('id');

        return this.tourService.getTourRecord(name) as Observable<any>;
    }
}