import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeThreeComponent } from './components/home-three/home-three.component';
import { JobsnewComponent } from './components/jobsnew/jobsnew.component';
import { FavouriteJobsComponent } from './components/favourite-jobs/favourite-jobs.component';
import { EmployersComponent } from './components/employers/employers.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';



const routes: Routes = [
    {path: '', component: HomeThreeComponent},
    {path: 'jobsnew', component: JobsnewComponent},
    {path: 'favourite-jobs', component: FavouriteJobsComponent},
    {path: 'employers', component: EmployersComponent} ,
    {path: 'dashboard', component: DashboardComponent}   
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
    exports: [RouterModule]
})
export class AppRoutingModule {}