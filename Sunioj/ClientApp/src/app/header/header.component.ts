import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../services/settings.service';

@Component({
    selector: 'app-header',
    templateUrl: 'header.component.html'
})
export class HeaderComponent implements OnInit {

    siteName: string;

    constructor(private settingsService: SettingsService) {

    }

    async ngOnInit(): Promise<void> {
        this.siteName = await this.settingsService.getSetting('site.name');
    }
}