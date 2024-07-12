import { Injectable } from '@angular/core';
import {
    Resolve,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';

import { Observable } from 'rxjs';
import { ActiveTourService } from './activetour.service';

@Injectable()
export class ActiveTourDetailResolverService implements Resolve<any> {
    constructor(private activetourService: ActiveTourService) { }
    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> {
        const name = route.paramMap.get('id');

        return this.activetourService.getActiveTour(name) as Observable<any>;
    }
}