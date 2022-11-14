import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeThreeComponent } from './components/home-three/home-three.component';
import { JobsComponent } from './components/jobs/jobs.component';
import { JobsnewComponent } from './components/jobsnew/jobsnew.component';

const routes: Routes = [
    {path: '', component: HomeThreeComponent},
    {path: 'jobs', component: JobsComponent},
    {path: 'jobsnew', component: JobsnewComponent}
    
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
    exports: [RouterModule]
})
export class AppRoutingModule {}