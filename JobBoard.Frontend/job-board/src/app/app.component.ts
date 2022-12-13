import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart, NavigationCancel, NavigationEnd } from '@angular/router';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { filter } from 'rxjs/operators';
import {OidcSecurityService} from 'angular-auth-oidc-client';
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
    public isAuth: boolean = false;

    constructor(private router: Router, public oidcSecurityService: OidcSecurityService) {
    }

    ngOnInit(){
        this.oidcSecurityService.checkAuth().subscribe((result) => {
            localStorage.setItem('token', result.accessToken);
            this.isAuth = result.isAuthenticated;
            
            let role = JSON.parse(window.atob(result.accessToken.split('.')[1])).role;
            localStorage.setItem('role', role);     
            
            console.log('access token: ' + result.accessToken);
          });
        this.recallJsFuntions();
    }

    login(){
        this.oidcSecurityService.authorize();
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
