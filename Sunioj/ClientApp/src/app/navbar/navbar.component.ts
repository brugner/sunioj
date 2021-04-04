import { SettingsService } from './../services/settings.service';
import { Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

    private readonly modeCacheKey = 'sj-mode';

    @ViewChild('darkModeCheckbox') darkModeCheckbox: ElementRef;

    siteImage: string;
    editorBio: string;
    socialsLinkedIn: string;
    socialsGithub: string;
    socialsStackoverflow: string;
    socialsTwitter: string;
    socialsFacebook: string;
    socialsInstagram: string;
    settingsLoaded: boolean = false;

    constructor(
        public authService: AuthService,
        private settingsService: SettingsService,
        private renderer: Renderer2) {

    }

    async ngOnInit(): Promise<void> {
        this.siteImage = await this.settingsService.getSetting('site.image');
        this.editorBio = await this.settingsService.getSetting('editor.bio');
        this.socialsLinkedIn = await this.settingsService.getSetting('socials.linkedin');
        this.socialsGithub = await this.settingsService.getSetting('socials.github');
        this.socialsStackoverflow = await this.settingsService.getSetting('socials.stackoverflow');
        this.socialsTwitter = await this.settingsService.getSetting('socials.twitter');
        this.socialsFacebook = await this.settingsService.getSetting('socials.facebook');
        this.socialsInstagram = await this.settingsService.getSetting('socials.instagram');
        this.settingsLoaded = true;
    }

    ngAfterViewInit() {
        this.loadMode();
    }

    logout(): void {
        this.authService.logout();
    }

    onDarkModeChanged($event: any): void {
        if ($event.target.checked) {
            this.renderer.addClass(document.body, 'dark-mode');
            localStorage.setItem(this.modeCacheKey, 'dark-mode');
        } else {
            this.renderer.removeClass(document.body, 'dark-mode');
            localStorage.removeItem(this.modeCacheKey);
        }
    }

    private loadMode() {
        const mode = localStorage.getItem(this.modeCacheKey);

        if (mode) {
            this.renderer.addClass(document.body, mode);
            this.darkModeCheckbox.nativeElement.checked = true;
        }
    }
}
