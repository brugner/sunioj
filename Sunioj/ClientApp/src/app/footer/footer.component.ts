import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../services/settings.service';

@Component({
    selector: 'app-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

    year: number = 0;
    siteName: string;

    constructor(private settingsService: SettingsService) {

    }

    async ngOnInit(): Promise<void> {
        this.year = new Date().getFullYear();

        this.siteName = await this.settingsService.getSetting('site.name');
    }
}
