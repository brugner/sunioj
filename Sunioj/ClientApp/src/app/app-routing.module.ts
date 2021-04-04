import { ResumeComponent } from './resume/resume.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AboutMeComponent } from './about-me/about-me.component';
import { BlogComponent } from './blog/blog.component';
import { ContactComponent } from './contact/contact.component';
import { ErrorComponent } from './error/error.component';
import { ViewPostComponent } from './view-post/view-post.component';
import { LoginComponent } from './auth/login/login.component';
import { EditPostComponent } from './edit-post/edit-post.component';
import { AuthGuard } from './auth/auth.guard';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MessageComponent } from './message/message.component';
import { PostsComponent } from './posts/posts.component';
import { MessagesComponent } from './messages/messages.component';
import { ServicePackagesComponent } from './service-packages/service-packages.component';
import { EditServicePackageComponent } from './edit-service-package/edit-service-package.component';
import { ServicesAndPricingComponent } from './services-and-pricings/services-and-pricing.component';
import { ServiceFaqsComponent } from './service-faqs/service-faqs.component';
import { SettingsComponent } from './settings/settings.component';
import { ResumeConfigComponent } from './resume-config/resume-config.component';

const routes: Routes = [
    { path: '', pathMatch: 'full', redirectTo: 'about-me' },
    { path: 'about-me', component: AboutMeComponent },
    { path: 'resume', component: ResumeComponent },
    { path: 'blog', component: BlogComponent },
    { path: 'blog/tag/:tag', component: BlogComponent },
    { path: 'blog/post/:slug', component: ViewPostComponent },
    { path: 'contact', component: ContactComponent },
    { path: 'services-and-pricing', component: ServicesAndPricingComponent },
    { path: 'auth/login', component: LoginComponent },
    { path: 'error/0', component: ErrorComponent },
    { path: 'error/401', component: ErrorComponent },
    { path: 'error/404', component: ErrorComponent },
    { path: 'error/500', component: ErrorComponent },

    { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
    { path: 'posts', component: PostsComponent, canActivate: [AuthGuard] },
    { path: 'posts/:id', component: EditPostComponent, canActivate: [AuthGuard] },
    { path: 'messages', component: MessagesComponent, canActivate: [AuthGuard] },
    { path: 'messages/:id', component: MessageComponent, canActivate: [AuthGuard] },
    { path: 'service-packages', component: ServicePackagesComponent, canActivate: [AuthGuard] },
    { path: 'service-packages/:id', component: EditServicePackageComponent, canActivate: [AuthGuard] },
    { path: 'service-faqs', component: ServiceFaqsComponent, canActivate: [AuthGuard] },
    { path: 'settings', component: SettingsComponent, canActivate: [AuthGuard] },
    { path: 'resume-config', component: ResumeConfigComponent, canActivate: [AuthGuard] },
    { path: '**', redirectTo: 'error/404' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' })],
    exports: [RouterModule]
})
export class AppRoutingModule {

}
