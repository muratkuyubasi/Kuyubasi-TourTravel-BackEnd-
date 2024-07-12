import { Injectable } from '@angular/core';
import {
    Resolve,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';

import { Observable } from 'rxjs';
import { DepartureService } from './departure.service';

@Injectable()
export class DepartureDetailResolverService implements Resolve<any> {
    constructor(private departureService: DepartureService) { }
    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> {
        const name = route.paramMap.get('id');

        return this.departureService.getDepartureRecord(name) as Observable<any>;
    }
}