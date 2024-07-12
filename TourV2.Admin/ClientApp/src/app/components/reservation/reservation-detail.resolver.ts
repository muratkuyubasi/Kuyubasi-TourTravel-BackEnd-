import { Injectable } from '@angular/core';
import {
    Resolve,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';

import { Observable } from 'rxjs';
import { ReservationService } from './reservation.service';

@Injectable()
export class ReservationDetailResolverService implements Resolve<any> {
    constructor(private reservationService: ReservationService) { }
    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> {
        const name = route.paramMap.get('id');

        return this.reservationService.getReservation(name) as Observable<any>;
    }
}