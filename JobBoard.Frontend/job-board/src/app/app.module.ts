import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeThreeComponent } from './components/pages/home-three/home-three.component';
import { NavbarStyleThreeComponent } from './components/common/navbar-style-three/navbar-style-three.component';
import { FooterStyleTwoComponent } from './components/common/footer-style-two/footer-style-two.component';
import { JobsComponent } from './components/pages/jobs/jobs.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeThreeComponent,
    NavbarStyleThreeComponent,
    FooterStyleTwoComponent,
    JobsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
