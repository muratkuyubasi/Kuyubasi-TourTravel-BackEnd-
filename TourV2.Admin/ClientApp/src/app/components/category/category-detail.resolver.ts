import { Injectable } from '@angular/core';
import {
    Resolve,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';

import { Observable } from 'rxjs';
import { CategoryService } from './category.service';

@Injectable()
export class CategoryDetailResolverService implements Resolve<any> {
    constructor(private categoryService: CategoryService) { }
    resolve(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ): Observable<any> {
        const name = route.paramMap.get('id');

        return this.categoryService.getCategoryRecord(name) as Observable<any>;
    }
}