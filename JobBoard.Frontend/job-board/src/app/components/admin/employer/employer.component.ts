import { Component, OnInit } from "@angular/core";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { Client, Employer } from "src/app/api/api";

@Component({
  selector: "app-employer",
  templateUrl: "./employer.component.html",
  styleUrls: ["./employer.component.scss"],
})
export class EmployerComponent implements OnInit {
  public employers: Employer[];

  constructor(
    private client: Client,
    public oidcSecurityService: OidcSecurityService
  ) {}

  ngOnInit(): void {
    this.client.getEmployers("1").subscribe((res) => {
      this.employers = res.employers as Employer[];
    });
  }

  ban(employer: Employer) {
    employer.isBan = !employer.isBan;
    this.client.banEmployer(employer.id, "1").subscribe((res) => {
      console.log("succesfuly banned");
    });
  }

  exit() {
    localStorage.setItem('role', undefined);
    this.oidcSecurityService.logoff();
  }
}
