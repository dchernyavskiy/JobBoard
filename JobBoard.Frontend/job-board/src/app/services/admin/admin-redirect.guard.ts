import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdminRedirectGuard implements CanActivate {

  constructor(private oidcSecurityService: OidcSecurityService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      console.log('i am in admin guard');
      let role = localStorage.getItem('role');
      if(role == 'SystemAdministrator'){
        console.log('i am in admin guard');
        this.router.navigateByUrl('/admin');
        return false;
      }
      return true;
  }
  
}
