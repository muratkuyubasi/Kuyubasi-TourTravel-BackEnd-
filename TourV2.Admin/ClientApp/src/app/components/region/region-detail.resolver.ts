import { Injectable } from '@angular/core';
import {
    Resolve,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';

import { Observable } from 'rxjs';
import { RegionService } from './region.service';

@Injectable()
export class RegionDetailResolverService implements Resolve<any> {
    constructor(private regionService: RegionService) { }
    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> {
        const name = route.paramMap.get('id');

        return this.regionService.getRegionRecord(name) as Observable<any>;
    }
}