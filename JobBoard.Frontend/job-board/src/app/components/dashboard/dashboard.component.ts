import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { OidcSecurityService } from "angular-auth-oidc-client";
import { AppliedJobLookupDto, JobLookupDto, Client, EmployeeVm, EmployerVm, LikedJobLookupDto } from "src/app/api/api";

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.scss"],
})
export class DashboardComponent implements OnInit {
  public employee: EmployeeVm = {};
  public employer: EmployerVm = {};
  public appliedJobs: AppliedJobLookupDto[] = [];
  public likedJobs: LikedJobLookupDto[] = [];
  public isEmployee: boolean = false;
  constructor(public client: Client, public oidcSecurityService: OidcSecurityService) {}

  ngOnInit(): void {
    this.isEmployee = localStorage.getItem('role') == "Employee";
    this.client.get("1").subscribe((res) => {
      this.employee = res as EmployeeVm;
    });

    this.client.getAppliedJobs("1").subscribe((res) => {
      this.appliedJobs = res.jobs as AppliedJobLookupDto[];
    });
    this.client.getLikedJobs("1").subscribe(res =>{
      this.likedJobs = res.jobs as LikedJobLookupDto[];
    })

    // for employer
    this.client.getProfile("1").subscribe((res) => {
      this.employer = res as EmployerVm;
      console.log((res as EmployerVm).jobs);
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