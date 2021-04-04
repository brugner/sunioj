import { environment } from './../../environments/environment';
import { SettingForUpdate } from './../models/settings/setting-for-update.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { SettingsService } from '../services/settings.service';
import { Setting } from '../models/settings/setting.model';
import { ImageSettingForUpdate } from '../models/settings/image-setting-for-update.model';

@Component({
    selector: 'app-settings',
    templateUrl: 'settings.component.html'
})
export class SettingsComponent implements OnInit {

    siteImage: File;
    siteImageUrl: string = '';
    aboutMeImage: File;
    aboutMeImageUrl: string = '';
    isSubmittingImage: boolean = false;

    public editorConfig = {
        height: 500,
        menubar: false,
        plugins: [
            'advlist autolink lists link image charmap print preview anchor',
            'searchreplace visualblocks code fullscreen',
            'insertdatetime media table paste code help wordcount'
        ],
        toolbar:
            'undo redo | formatselect | bold italic backcolor | \
      alignleft aligncenter alignright alignjustify | \
      bullist numlist outdent indent | removeformat | help',
        paste_data_images: true
    };

    isLoading: boolean = true;
    isSubmitting: boolean = false;
    settingsForm: FormGroup;
    skills: string[] = [];

    constructor(
        private settingsService: SettingsService,
        private toastrService: ToastrService,
        private formBuilder: FormBuilder) {

    }

    async ngOnInit(): Promise<void> {
        this.buildSettingsForm();

        const settings = await this.settingsService.getAll();
        this.setFormFromSettings(settings);
    }

    onSiteImageChanged(event: any) {
        if (event.target.files.length === 0) {
            return;
        }

        const file: File = event.target.files[0];
        const reader = new FileReader();

        reader.addEventListener('load', (event: any) => {
            this.siteImage = file;
            this.siteImageUrl = event.target.result;

        });

        reader.readAsDataURL(file);
    }

    onAboutMeImageChanged(event: any) {
        if (event.target.files.length === 0) {
            return;
        }

        const file: File = event.target.files[0];
        const reader = new FileReader();

        reader.addEventListener('load', (event: any) => {
            this.aboutMeImage = file;
            this.aboutMeImageUrl = event.target.result;

        });

        reader.readAsDataURL(file);
    }

    uploadSettingImage(settingName: string): void {
        this.isSubmittingImage = true;

        const imageSetting = new ImageSettingForUpdate();
        imageSetting.name = settingName;
        imageSetting.image = settingName === 'site.image' ? this.siteImage : this.aboutMeImage;

        this.settingsService.updateImage(imageSetting)
            .subscribe(async () => {
                this.isSubmittingImage = false;
                const siteName = await this.settingsService.getSetting('site.name');
                this.toastrService.success('Image updated', siteName);
            }, () => {
                this.isSubmittingImage = false;
            });
    }

    submitForm(): void {
        this.isSubmitting = true;
        const settingsForUpdate = this.getSettingsForUpdate();

        this.settingsService.update(settingsForUpdate)
            .subscribe(async (settings: Setting[]) => {
                this.isSubmitting = false;
                const siteName = await this.settingsService.getSetting('site.name');
                this.toastrService.success('Settings updated', siteName);
                this.setFormFromSettings(settings);
            }, () => {
                this.isSubmitting = false;
            });
    }

    addSkill(): void {
        const name = this.skillName.value;
        const knowledge = this.skillKnowledge.value;

        if (name === '') {
            return;
        }

        this.skills.push(`${name}:${knowledge}`);

        this.skillName.setValue('');
        this.skillKnowledge.setValue(50);
    }

    removeSkill(skill: string): void {
        const index = this.skills.findIndex(x => x === skill);
        this.skills.splice(index, 1);
    }

    onSkillNameChanged(value: string) {
        this.skillName.setValue(value.replace(';', ''))
    }

    private setFormFromSettings(settings: Setting[]) {
        this.siteName.setValue(settings.find(x => x.name === 'site.name').value);
        this.siteImageUrl = settings.find(x => x.name === 'site.image').value;
        this.editorName.setValue(settings.find(x => x.name === 'editor.name').value);
        this.editorEmail.setValue(settings.find(x => x.name === 'editor.email').value);
        this.editorTitle.setValue(settings.find(x => x.name === 'editor.title').value);
        this.editorSubtitle.setValue(settings.find(x => x.name === 'editor.subtitle').value);
        this.editorBio.setValue(settings.find(x => x.name === 'editor.bio').value);
        this.aboutMeWhatIDo.setValue(settings.find(x => x.name === 'about-me.what-i-do').value);
        this.expertiseSummary.setValue(settings.find(x => x.name === 'about-me.expertise.summary').value);
        this.skills = settings.find(x => x.name === 'about-me.expertise.skills').value.split(';');
        this.socialsLinkedIn.setValue(settings.find(x => x.name === 'socials.linkedin').value);
        this.socialsGithub.setValue(settings.find(x => x.name === 'socials.github').value);
        this.socialsStackOverflow.setValue(settings.find(x => x.name === 'socials.stackoverflow').value);
        this.socialsTwitter.setValue(settings.find(x => x.name === 'socials.twitter').value);
        this.socialsFacebook.setValue(settings.find(x => x.name === 'socials.facebook').value);
        this.socialsInstagram.setValue(settings.find(x => x.name === 'socials.instagram').value);
    }

    private getSettingsForUpdate(): SettingForUpdate[] {
        var settings: SettingForUpdate[] = [
            {
                name: 'site.name',
                value: this.siteName.value
            },
            {
                name: 'editor.name',
                value: this.editorName.value
            },
            {
                name: 'editor.email',
                value: this.editorEmail.value
            },
            {
                name: 'editor.title',
                value: this.editorTitle.value
            },
            {
                name: 'editor.subtitle',
                value: this.editorSubtitle.value
            },
            {
                name: 'editor.bio',
                value: this.editorBio.value
            },
            {
                name: 'about-me.what-i-do',
                value: this.aboutMeWhatIDo.value
            },
            {
                name: 'about-me.expertise.skills',
                value: this.skills.join(';')
            },
            {
                name: 'about-me.expertise.summary',
                value: this.expertiseSummary.value
            },
            {
                name: 'socials.linkedin',
                value: this.socialsLinkedIn.value
            },
            {
                name: 'socials.github',
                value: this.socialsGithub.value
            },
            {
                name: 'socials.stackoverflow',
                value: this.socialsStackOverflow.value
            },
            {
                name: 'socials.twitter',
                value: this.socialsTwitter.value
            },
            {
                name: 'socials.facebook',
                value: this.socialsFacebook.value
            },
            {
                name: 'socials.instagram',
                value: this.socialsInstagram.value
            }
        ]

        return settings;
    }

    private buildSettingsForm() {
        this.settingsForm = this.formBuilder.group({
            siteName: new FormControl('', [Validators.required, Validators.maxLength(15)]),
            editorName: new FormControl('', [Validators.required, Validators.maxLength(30)]),
            editorEmail: new FormControl('', [Validators.required, Validators.maxLength(100)]),
            editorTitle: new FormControl('', [Validators.required, Validators.maxLength(40)]),
            editorSubtitle: new FormControl('', [Validators.required, Validators.maxLength(250)]),
            editorBio: new FormControl('', [Validators.required, Validators.maxLength(120)]),
            aboutMeWhatIDo: new FormControl('', [Validators.required]),
            expertiseSummary: new FormControl('', [Validators.required, Validators.maxLength(400)]),
            skillName: new FormControl(''),
            skillKnowledge: new FormControl(50, [Validators.required, Validators.min(0), Validators.max(100)]),
            socialsLinkedIn: new FormControl(''),
            socialsGithub: new FormControl(''),
            socialsStackOverflow: new FormControl(''),
            socialsTwitter: new FormControl(''),
            socialsFacebook: new FormControl(''),
            socialsInstagram: new FormControl(''),
        });
    }

    get siteName() {
        return this.settingsForm.get('siteName');
    }

    get editorName() {
        return this.settingsForm.get('editorName');
    }

    get editorEmail() {
        return this.settingsForm.get('editorEmail');
    }

    get editorTitle() {
        return this.settingsForm.get('editorTitle');
    }

    get editorSubtitle() {
        return this.settingsForm.get('editorSubtitle');
    }

    get editorBio() {
        return this.settingsForm.get('editorBio');
    }

    get aboutMeWhatIDo() {
        return this.settingsForm.get('aboutMeWhatIDo');
    }

    get expertiseSummary() {
        return this.settingsForm.get('expertiseSummary');
    }

    get skillName() {
        return this.settingsForm.get('skillName');
    }

    get skillKnowledge() {
        return this.settingsForm.get('skillKnowledge');
    }

    get socialsLinkedIn() {
        return this.settingsForm.get('socialsLinkedIn');
    }

    get socialsGithub() {
        return this.settingsForm.get('socialsGithub');
    }

    get socialsStackOverflow() {
        return this.settingsForm.get('socialsStackOverflow');
    }

    get socialsTwitter() {
        return this.settingsForm.get('socialsTwitter');
    }

    get socialsFacebook() {
        return this.settingsForm.get('socialsFacebook');
    }

    get socialsInstagram() {
        return this.settingsForm.get('socialsInstagram');
    }
}