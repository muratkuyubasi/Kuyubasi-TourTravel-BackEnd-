import { Injectable } from '@angular/core';
import {
    Resolve,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';

import { Observable } from 'rxjs';
import { PeriodService } from './period.service';

@Injectable()
export class PeriodDetailResolverService implements Resolve<any> {
    constructor(private periodService: PeriodService) { }
    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> {
        const name = route.paramMap.get('id');

        return this.periodService.getPeriodRecord(name) as Observable<any>;
    }
}