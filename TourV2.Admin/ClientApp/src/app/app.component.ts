import { Component, OnInit } from '@angular/core';
import { OnlineUser } from '@core/domain-classes/online-user';
import { UserAuth } from '@core/domain-classes/user-auth';
import { SecurityService } from '@core/security/security.service';
import { SignalrService } from '@core/services/signalr.service';
import { TranslationService } from '@core/services/translation.service';
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent } from './base.component';

import { Router, NavigationStart, NavigationCancel, NavigationEnd } from '@angular/router';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { filter } from 'rxjs/operators';
declare let $: any;

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    providers: [
        Location, {
            provide: LocationStrategy,
            useClass: PathLocationStrategy
        }
    ]
})
export class AppComponent extends BaseComponent implements OnInit {
    location: any;
    routerSubscription: any;

    constructor(private router: Router, private securityService: SecurityService,
        public translate: TranslateService,
        private translationService: TranslationService) {
            super();
            translate.addLangs(['tr','en', 'es', 'ar', 'ru', 'cn', 'ja', 'ko']);
            translate.setDefaultLang('tr');
            this.setLanguage();
    }

    setLanguage() {
        const currentLang = this.translationService.getSelectedLanguage();
        if (currentLang) {
          this.sub$.sink = this.translationService.setLanguage(currentLang)
          .subscribe(() => { });
        }
        else {
          const browserLang = this.translate.getBrowserLang();
          const lang = browserLang.match(/tr|en|es|ar|ru|cn|ja|ko/) ? browserLang : 'tr';
          this.sub$.sink = this.translationService.setLanguage(lang).subscribe(() => { });
        }
      }
      
    ngOnInit(){
        this.recallJsFuntions();
    }

    recallJsFuntions() {
        this.routerSubscription = this.router.events
        .pipe(filter(event => event instanceof NavigationEnd || event instanceof NavigationCancel))
        .subscribe(event => {
            $.getScript('../assets/js/custom.js');
            this.location = this.router.url;
            if (!(event instanceof NavigationEnd)) {
                return;
            }
            window.scrollTo(0, 0);
        });
    }
}