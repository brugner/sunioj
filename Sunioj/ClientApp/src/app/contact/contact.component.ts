import { ToastrService } from 'ngx-toastr';
import { MessagesService } from './../services/messages.service';
import { ServicePackagesService } from './../services/service-packages.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ServicePackage } from '../models/service-packages/service-package.model';
import { Setting } from '../models/settings/setting.model';
import { SettingsService } from '../services/settings.service';
import { MessageForCreation } from '../models/messages/message-for-creation.model';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html'
})
export class ContactComponent implements OnInit {

  contactForm: FormGroup;
  isSubmitting: boolean = false;
  servicePackages: ServicePackage[] = [];

  siteName: string;
  editorEmail: string;
  socialsLinkedIn: string;
  socialsGithub: string;
  socialsStackoverflow: string;
  socialsTwitter: string;
  socialsFacebook: string;
  socialsInstagram: string;

  settingsLoaded: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private settingsService: SettingsService,
    private servicePackagesService: ServicePackagesService,
    private messagesService: MessagesService,
    private toastrService: ToastrService) {

  }

  async ngOnInit(): Promise<void> {
    this.buildContactForm();

    this.servicePackagesService.getAll()
      .subscribe(packages => this.servicePackages = packages);

    this.siteName = await this.settingsService.getSetting('site.name');
    this.editorEmail = await this.settingsService.getSetting('editor.email');
    this.socialsLinkedIn = await this.settingsService.getSetting('socials.linkedin');
    this.socialsGithub = await this.settingsService.getSetting('socials.github');
    this.socialsStackoverflow = await this.settingsService.getSetting('socials.stackoverflow');
    this.socialsTwitter = await this.settingsService.getSetting('socials.twitter');
    this.socialsFacebook = await this.settingsService.getSetting('socials.facebook');
    this.socialsInstagram = await this.settingsService.getSetting('socials.instagram');
    this.settingsLoaded = true;
  }

  send(): void {
    this.isSubmitting = true;

    const message = new MessageForCreation();
    message.name = this.name.value;
    message.email = this.email.value;
    message.servicePackageId = this.servicePackageId.value;
    message.body = this.message.value;

    if (message.servicePackageId === 0) {
      message.servicePackageId = null;
    }

    this.messagesService.create(message)
      .subscribe(() => {
        this.toastrService.success('Message sent', this.siteName);
        this.cleanContactForm();
        this.isSubmitting = false;
      });
  }

  private buildContactForm(): void {
    this.contactForm = this.formBuilder.group({
      name: new FormControl('', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]),
      email: new FormControl('', [Validators.required, Validators.email, Validators.maxLength(100)]),
      servicePackageId: new FormControl(0, [Validators.required]),
      message: new FormControl('', [Validators.required])
    });
  }

  private cleanContactForm(): void {
    this.name.setValue('');
    this.email.setValue('');
    this.servicePackageId.setValue(0);
    this.message.setValue('');
  }

  get name() {
    return this.contactForm.get('name');
  }

  get email() {
    return this.contactForm.get('email');
  }

  get servicePackageId() {
    return this.contactForm.get('servicePackageId');
  }

  get message() {
    return this.contactForm.get('message');
  }
}
