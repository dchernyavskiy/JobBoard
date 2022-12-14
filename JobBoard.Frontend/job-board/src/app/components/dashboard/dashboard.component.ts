import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { AppliedJobLookupDto, JobLookupDto, Client, EmployeeVm, EmployerVm } from "src/app/api/api";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {
  public employee: EmployeeVm = {};
  public employer: EmployerVm = {};
  public appliedJobs: AppliedJobLookupDto[] = [];

  constructor(public client: Client, public oidcSecurityService: OidcSecurityService) {}

  ngOnInit(): void {
    
    this.client.get2("1").subscribe((res) => {
      this.employee = res as EmployeeVm;
    });

    this.client.getAppliedJobs("1").subscribe((res) => {
      this.appliedJobs = res.jobs as AppliedJobLookupDto[];
    });

    // for employer
    this.oidcSecurityService.checkAuth().subscribe(res => {
      this.client.get2(res.userData.sub, "1").subscribe((res) => {
        this.employer = res as EmployerVm;
      });
    });
  }

  updateEmployee() {
    this.client.update2("1", this.employee).subscribe((res) => {
    });
  }

  updateEmployer() {
    this.client.update3("1", this.employer).subscribe((res) => {
      console.log("succesfuly employer updated");
    });
  }

  logout() {
    this.oidcSecurityService.logoff();
  }
}