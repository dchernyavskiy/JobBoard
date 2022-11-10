import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AuthModule, LogLevel } from "angular-auth-oidc-client";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { HomeThreeComponent } from "./components/pages/home-three/home-three.component";
import { FooterStyleTwoComponent } from "./components/common/footer-style-two/footer-style-two.component";
import { environment } from "../environments/environment";
import { API_BASE_URL } from "./api/api";
import { JobCardComponent } from './job-card/job-card.component';

@NgModule({
  declarations: [AppComponent, HomeThreeComponent, FooterStyleTwoComponent, JobCardComponent, JobCardComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthModule.forRoot({
      config: {
        authority: "https://localhost:5002",
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
