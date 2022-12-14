import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeThreeComponent } from "./components/home-three/home-three.component";
import { JobsnewComponent } from "./components/jobsnew/jobsnew.component";
import { FavouriteJobsComponent } from "./components/favourite-jobs/favourite-jobs.component";
import { EmployersComponent } from "./components/employers/employers.component";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { PostAJobComponent } from "./components/post-a-job/post-a-job.component";
import { JobDetailsComponent } from "./components/job-details/job-details.component";
import { EmployersDetailsComponent } from "./components/employers-details/employers-details.component";
import { AuthGuard } from "./services/auth/auth.guard";
import { EmployerGuard } from "./services/employer/employer.guard";
import { EmployerComponent } from "./components/admin/employer/employer.component";
import { EmployeeComponent } from "./components/admin/employee/employee.component";
import { AdminLayoutComponent } from "./components/layouts/admin-layout/admin-layout.component";
import { SystemAdministratorGuard } from "./services/system-administrator/system-administrator.guard";
import { AdminRedirectGuard } from "./services/admin/admin-redirect.guard";

const routes: Routes = [
  {
    path: "",
    component: HomeThreeComponent,
    canActivate: [AdminRedirectGuard],
  },
  { path: "jobs", component: JobsnewComponent },
  {
    path: "jobs?keyword=:keyword&category=:categoryId&location=:location",
    component: JobsnewComponent,
  },
  { path: "favourite-jobs", component: FavouriteJobsComponent },
  { path: "employers", component: EmployersComponent },
  { path: "dashboard", component: DashboardComponent },
  {
    path: "post-a-job",
    component: PostAJobComponent,
    canActivate: [AuthGuard, EmployerGuard],
  },
  { path: "jobs/:id", component: JobDetailsComponent },
  { path: "employer-details", component: EmployersDetailsComponent },
  {
    path: "admin",
    redirectTo: "admin/employers"
  },
  {
    path: "admin/employers",
    component: EmployerComponent,
    canActivate: [SystemAdministratorGuard],
  },
  {
    path: "admin/employees",
    component: EmployeeComponent,
    canActivate: [SystemAdministratorGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: "legacy" })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
