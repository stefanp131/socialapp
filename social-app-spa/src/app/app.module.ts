import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { SharedModule } from './_modules/shared/shared.module';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MessageBoardComponent } from './social/message-board/message-board.component';
import { ProfileComponent } from './social/profile/profile.component';
import { HomeComponent } from './home/home.component';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { UserCardComponent } from './social/user-card/user-card.component';

@NgModule({
  declarations: [AppComponent, HeaderComponent, FooterComponent, LoginComponent, RegisterComponent, MessageBoardComponent, ProfileComponent, HomeComponent, UserCardComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    SharedModule,
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
