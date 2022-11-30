import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart, NavigationCancel, NavigationEnd } from '@angular/router';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { filter } from 'rxjs/operators';
import {OidcSecurityService} from 'angular-auth-oidc-client';
import {environment} from './../environments/environment';
import { JobService } from './services/job.service';
import {JobLookupDto} from './api/api';
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
export class AppComponent implements OnInit {
    location: any;
    routerSubscription: any;

    constructor(private router: Router, public oidcSecurityService: OidcSecurityService) {
    }

    ngOnInit(){
        this.oidcSecurityService.checkAuth().subscribe((result) => {
            if (!result.isAuthenticated) {
              this.oidcSecurityService.authorize();
            }
            localStorage.setItem('token', result.accessToken);
      
            let role = JSON.parse(window.atob(result.accessToken.split('.')[1])).role;
            localStorage.setItem('role', role);
            console.log(role);
      
            console.log('access token: ' + result.accessToken);
            console.log(result.userData);
          });
        this.recallJsFuntions();

    }

    login(){
        this.oidcSecurityService.authorize();
    }

    logout() {
        this.oidcSecurityService.logoff();
      }

    recallJsFuntions() {
        this.router.events
        .subscribe((event) => {
            if ( event instanceof NavigationStart ) {
                $('.loader').fadeIn('slow');
            }
        });
        this.routerSubscription = this.router.events
        .pipe(filter(event => event instanceof NavigationEnd || event instanceof NavigationCancel))
        .subscribe(event => {
            $.getScript('../assets/js/custom.js');
            $('.loader').fadeOut('slow');
            this.location = this.router.url;
            if (!(event instanceof NavigationEnd)) {
                return;
            }
            window.scrollTo(0, 0);
        });
    }
}
