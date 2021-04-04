import { SettingsService } from './services/settings.service';
import { ServiceFaqsService } from './services/service-faqs.service';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AboutMeComponent } from './about-me/about-me.component';
import { DataTablesModule } from 'angular-datatables';
import { AppRoutingModule } from './app-routing.module';
import { AuthService } from './services/auth.service';
import { PostsService } from './services/posts.service';
import { ApiUrlInterceptor } from './interceptors/api-url.interceptor';
import { JwtInterceptor } from './interceptors/jwt-auth.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ResumeComponent } from './resume/resume.component';
import { BlogComponent } from './blog/blog.component';
import { ContactComponent } from './contact/contact.component';
import { ErrorComponent } from './error/error.component';
import { TimeAgoPipe } from './pipes/time-ago.pipe';
import { ReadingTimePipe } from './pipes/reading-time.pipe';
import { ViewPostComponent } from './view-post/view-post.component';
import { EditPostComponent } from './edit-post/edit-post.component';
import { EditorModule } from '@tinymce/tinymce-angular';
import { AutocompleteLibModule } from 'angular-ng-autocomplete';
import { RawHtmlPipe } from './pipes/raw-html.pipe';
import { DashboardComponent } from './dashboard/dashboard.component';
import { YesNoPipe } from './pipes/yes-no.pipe';
import { LoginComponent } from './auth/login/login.component';
import { MessagesService } from './services/messages.service';
import { MessageComponent } from './message/message.component';
import { ServicePackagesService } from './services/service-packages.service';
import { PostsComponent } from './posts/posts.component';
import { MessagesComponent } from './messages/messages.component';
import { ServicePackagesComponent } from './service-packages/service-packages.component';
import { EditServicePackageComponent } from './edit-service-package/edit-service-package.component';
import { ServicesAndPricingComponent } from './services-and-pricings/services-and-pricing.component';
import { ServiceFaqsComponent } from './service-faqs/service-faqs.component';
import { SettingsComponent } from './settings/settings.component';
import { ResumeService } from './services/resume.service';
import { ResumeConfigComponent } from './resume-config/resume-config.component';

@NgModule({
  declarations: [
    AppComponent,
    AboutMeComponent,
    FooterComponent,
    HeaderComponent,
    NavbarComponent,
    ResumeComponent,
    BlogComponent,
    ContactComponent,
    ServicesAndPricingComponent,
    ErrorComponent,
    ReadingTimePipe,
    TimeAgoPipe,
    RawHtmlPipe,
    YesNoPipe,
    ViewPostComponent,
    LoginComponent,
    EditPostComponent,
    DashboardComponent,
    PostsComponent,
    MessagesComponent,
    ServicePackagesComponent,
    EditServicePackageComponent,
    MessageComponent,
    ServiceFaqsComponent,
    SettingsComponent,
    ResumeConfigComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({ closeButton: true, positionClass: 'toast-bottom-right' }),
    EditorModule,
    AutocompleteLibModule,
    DataTablesModule
  ],
  providers: [
    AuthService,
    PostsService,
    MessagesService,
    ServicePackagesService,
    ServiceFaqsService,
    ResumeService,
    SettingsService,
    { provide: HTTP_INTERCEPTORS, useClass: ApiUrlInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: APP_INITIALIZER, useFactory: resourceProviderFactory, deps: [SettingsService], multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}

export function resourceProviderFactory(provider: SettingsService) {
  return () => provider.getAll();
}