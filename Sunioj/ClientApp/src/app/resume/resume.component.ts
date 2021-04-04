import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';
import { Resume } from '../models/resume/resume.model';
import { Setting } from '../models/settings/setting.model';
import { ResumeService } from '../services/resume.service';
import { SettingsService } from '../services/settings.service';

@Component({
  selector: 'app-resume',
  templateUrl: './resume.component.html'
})
export class ResumeComponent implements OnInit {

  resume: Resume;
  resumeLoaded: boolean = false;

  socialsLinkedIn: string;
  socialsGithub: string;
  socialsStackoverflow: string;

  constructor(
    public authService: AuthService,
    private resumeService: ResumeService,
    private settingsService: SettingsService) {

  }

  async ngOnInit(): Promise<void> {
    this.resumeService.get()
      .subscribe((resume: Resume) => {
        this.resume = resume;
        this.resumeLoaded = true;
      });

    this.socialsLinkedIn = await this.settingsService.getSetting('socials.linkedin');
    this.socialsGithub = await this.settingsService.getSetting('socials.github');
    this.socialsStackoverflow = await this.settingsService.getSetting('socials.stackoverflow');
  }
}
