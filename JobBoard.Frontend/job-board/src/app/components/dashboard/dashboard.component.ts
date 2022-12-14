import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { AppliedJobLookupDto, Client, EmployeeVm } from "src/app/api/api";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {
  public employee: EmployeeVm = {};
  public appliedJobs: AppliedJobLookupDto[] = [];
  constructor(public client: Client, public oidcSecurityService: OidcSecurityService) {}

  ngOnInit(): void {
    
    this.client.get("1").subscribe((res) => {
      this.employee = res as EmployeeVm;
    });
    this.client.getAppliedJobs("1").subscribe((res) => {
      this.appliedJobs = res.jobs as AppliedJobLookupDto[];
    });
  }

  updateEmployee() {
    this.client.update2("1", this.employee).subscribe((res) => {
      console.log("succesfuly employee updated");
    });
  }

  logout() {
    this.oidcSecurityService.logoff();
  }
}
