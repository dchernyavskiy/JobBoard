import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeThreeComponent } from './components/pages/home-three/home-three.component';
import { JobsComponent } from './components/pages/jobs/jobs.component';

const routes: Routes = [
    {path: '', component: HomeThreeComponent},
    {path: 'jobs', component: JobsComponent}


];

@NgModule({
    imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
    exports: [RouterModule]
})
export class AppRoutingModule {}