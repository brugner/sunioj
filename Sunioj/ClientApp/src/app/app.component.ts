import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { SettingsService } from './services/settings.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

  constructor(
    private titleService: Title,
    private settingsService: SettingsService) {

  }

  async ngOnInit(): Promise<void> {
    const siteName = await this.settingsService.getSetting('site.name');
    this.titleService.setTitle(siteName);
  }
}
