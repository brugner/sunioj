import { PublishedPost } from '../models/posts/published-post.model';
import { PostsService } from '../services/posts.service';
import { Component, OnInit } from '@angular/core';
import { SettingsService } from '../services/settings.service';

@Component({
    selector: 'app-about-me',
    templateUrl: './about-me.component.html',
    styleUrls: ['./about-me.component.css']
})
export class AboutMeComponent implements OnInit {

    posts: PublishedPost[] = [];
    projects: PublishedPost[] = [];

    editorName: string;
    editorTitle: string;
    editorSubtitle: string;
    aboutMeImage: string;
    whatIDo: string;
    skills: string[] = [];
    expertiseSummary: string;

    settingsLoaded: boolean = false;
    postsLoaded: boolean = false;

    constructor(
        private postsService: PostsService,
        private settingsService: SettingsService) {

    }

    async ngOnInit(): Promise<void> {
        this.postsService.getAllPublished()
            .subscribe(posts => {
                this.posts = posts.slice(0, 3);
                this.projects = posts.filter(x => x.tags.some(k => k.name === 'project')).slice(0, 3);
                this.postsLoaded = true;
            });

        this.editorName = await this.settingsService.getSetting('editor.name');
        this.editorName = await this.settingsService.getSetting('editor.name');
        this.editorTitle = await this.settingsService.getSetting('editor.title');
        this.editorSubtitle = await this.settingsService.getSetting('editor.subtitle');
        this.aboutMeImage = await this.settingsService.getSetting('about-me.image');
        this.whatIDo = await this.settingsService.getSetting('about-me.what-i-do');

        const skills = await this.settingsService.getSetting('about-me.expertise.skills');
        if (skills.length > 0) {
            this.skills = skills.split(';')
        }

        this.expertiseSummary = await this.settingsService.getSetting('about-me.expertise.summary');
        this.settingsLoaded = true;
    }

    getSkillName(skill: string): string {
        return skill.split(':')[0];
    }

    getSkillKnowledge(skill: string): number {
        return Number.parseInt(skill.split(':')[1]);
    }

    getSkillKnowledgePercentage(skill: string): string {
        return this.getSkillKnowledge(skill) + '%';
    }

    getSkillColor(skill: string): string {
        const knowledge = this.getSkillKnowledge(skill);

        if (knowledge <= 33) {
            return "bg-danger";
        } else if (knowledge > 33 && knowledge <= 66) {
            return "bg-warning";
        } else {
            return "bg-success";
        }
    }
}
