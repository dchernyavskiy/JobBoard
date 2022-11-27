import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AuthModule, LogLevel } from "angular-auth-oidc-client";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { HomeThreeComponent } from "./components/home-three/home-three.component";
import { FooterStyleTwoComponent } from "./components/common/footer-style-two/footer-style-two.component";
import { environment } from "../environments/environment";
import { API_BASE_URL } from "./api/api";
import { JobCardComponent } from "./components/job-card/job-card.component";
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { JobsnewComponent } from './components/jobsnew/jobsnew.component';
import { FavouriteJobsComponent } from './components/favourite-jobs/favourite-jobs.component';
import { EmployersComponent } from './components/employers/employers.component';
import { EmployerCardComponent } from './components/employer-card/employer-card.component';






@NgModule({
  declarations: [AppComponent, HomeThreeComponent, FooterStyleTwoComponent, JobCardComponent, JobsnewComponent, FavouriteJobsComponent, EmployersComponent, EmployerCardComponent  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    
    AuthModule.forRoot({
      config: {
        authority: environment.authority,
        redirectUrl: window.location.origin,
        postLogoutRedirectUri: window.location.origin,
        clientId: "job-board-web-app",
        scope: "openid profile JobBoardWebApi",
        responseType: "code",
        silentRenew: true,
        useRefreshToken: true,
        logLevel: LogLevel.Debug,
      },
    }),
  ],
  providers: [
    {
        provide: API_BASE_URL,
        useValue: environment.apiUri
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
