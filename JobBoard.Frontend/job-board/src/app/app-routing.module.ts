import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { HomeThreeComponent } from "./components/home-three/home-three.component";
import { JobsnewComponent } from "./components/jobsnew/jobsnew.component";
import { FavouriteJobsComponent } from "./components/favourite-jobs/favourite-jobs.component";
import { EmployersComponent } from "./components/employers/employers.component";
import { DashboardComponent } from "./components/dashboard/dashboard.component";
import { PostAJobComponent } from "./components/post-a-job/post-a-job.component";
import { EmployersDetailsComponent } from './components/employers-details/employers-details.component';

const routes: Routes = [
  { path: "", component: HomeThreeComponent },
  { path: "jobs", component: JobsnewComponent },
  { path: "jobs?keyword=:keyword&category=:categoryId&location=:location", component: JobsnewComponent },
  { path: "favourite-jobs", component: FavouriteJobsComponent },
  { path: "employers", component: EmployersComponent },
  { path: "dashboard", component: DashboardComponent },
  { path: "post-a-job", component: PostAJobComponent },
  { path: 'employer-details', component: EmployersDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: "legacy" })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
