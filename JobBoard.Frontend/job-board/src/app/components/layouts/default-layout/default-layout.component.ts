import { Component, OnInit } from "@angular/core";
import { OidcSecurityService } from "angular-auth-oidc-client";

@Component({
  selector: "app-default-layout",
  templateUrl: "./default-layout.component.html",
  styleUrls: ["./default-layout.component.scss"],
})
export class DefaultLayoutComponent implements OnInit {
  public isAuth: boolean = false;

  constructor(public oidcSecurityService: OidcSecurityService) {}

  ngOnInit(): void {
    this.oidcSecurityService.checkAuth().subscribe((result) => {
      localStorage.setItem("token", result.accessToken);
      this.isAuth = result.isAuthenticated;

      let role = JSON.parse(window.atob(result.accessToken.split(".")[1])).role;
      localStorage.setItem("role", role);

      console.log("access token: " + result.accessToken);
    });
  }

  login() {
    this.oidcSecurityService.authorize();
  }
}
