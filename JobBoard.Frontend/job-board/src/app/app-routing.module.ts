import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeThreeComponent } from './components/home-three/home-three.component';
import { JobsnewComponent } from './components/jobsnew/jobsnew.component';
import { FavouriteJobsComponent } from './components/favourite-jobs/favourite-jobs.component';
import { EmployersComponent } from './components/employers/employers.component';
import { CandidatesDetailsComponent } from './components/candidates-details/candidates-details.component';
import { PostAJobComponent } from './components/post-a-job/post-a-job.component';
import { EmployersDetailsComponent } from './components/employers-details/employers-details.component';
import { CandidatesComponent } from './components/candidates/candidates.component';




const routes: Routes = [
    {path: '', component: HomeThreeComponent},
    {path: 'jobsnew', component: JobsnewComponent},
    {path: 'favourite-jobs', component: FavouriteJobsComponent},
    {path: 'employers', component: EmployersComponent},
    {path: 'candidate-details', component: CandidatesDetailsComponent},
    {path: 'post-a-job', component: PostAJobComponent},
    {path: 'employer-details', component: EmployersDetailsComponent},
    {path: 'candidates', component: CandidatesComponent}
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
    exports: [RouterModule]
})
export class AppRoutingModule {}