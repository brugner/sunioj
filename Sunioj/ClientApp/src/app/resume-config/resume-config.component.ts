import { ResumeInterest } from './../models/resume/resume-interest.model';
import { ResumeLanguage } from './../models/resume/resume-language.model';
import { ResumeSkill } from './../models/resume/resume-skill.model';
import { ResumeCourse } from './../models/resume/resume-course.model';
import { ResumeExperienceForUpdate } from './../models/resume/resume-experience-for-update.model';
import { Resume } from './../models/resume/resume.model';
import { ResumeService } from './../services/resume.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ResumeForUpdate } from '../models/resume/resume-for-update.model';
import { ResumeExperience } from '../models/resume/resume-experience.model';
import { ResumeEducationForUpdate } from '../models/resume/resume-education-for-update.model';
import { ResumeEducation } from '../models/resume/resume-education.model';
import { ResumeCourseForUpdate } from '../models/resume/resume-course-for-update.model';
import { ResumeSkillForUpdate } from '../models/resume/resume-skill-for-update.model';
import { ResumeLanguageForUpdate } from '../models/resume/resume-language-for-update.model';
import { ResumeInterestForUpdate } from '../models/resume/resume-interest-for-update.model';
import { SettingsService } from '../services/settings.service';

@Component({
    selector: 'app-resume-config',
    templateUrl: 'resume-config.component.html'
})
export class ResumeConfigComponent implements OnInit {

    siteName: string;
    resumeForm: FormGroup;
    isLoading: boolean = true;
    isSubmitting: boolean = false;

    monthsFrom: { id: number, name: string }[] = [];
    monthsTo: { id: number, name: string }[] = [];

    experience: ResumeExperience[] = [];
    experienceRemarks: string[] = [];

    education: ResumeEducation[] = [];
    educationRemarks: string[] = [];

    courses: ResumeCourse[] = [];

    skills: ResumeSkill[] = [];

    languages: ResumeLanguage[] = [];

    interests: ResumeInterest[] = [];

    constructor(
        private resumesService: ResumeService,
        private formBuilder: FormBuilder,
        private toastrService: ToastrService,
        private settingsService: SettingsService) {

    }

    async ngOnInit(): Promise<void> {
        this.siteName = await this.settingsService.getSetting('site.name');

        this.setMonths();
        this.buildResumeForm();

        this.resumesService.get()
            .subscribe(resume => {
                this.setFormFromResume(resume);
                this.isLoading = false;
            })
    }

    submitForm(): void {
        this.isSubmitting = true;

        const resumeForUpdate = this.getResumeFromForm();
        this.resumesService.update(resumeForUpdate)
            .subscribe((resume: Resume) => {
                this.setFormFromResume(resume);
                this.isSubmitting = false;
                this.toastrService.success('Resume updated', this.siteName);
            }, () => {
                this.isSubmitting = false;
            });
    }

    addExperience(): void {
        const experience = new ResumeExperienceForUpdate();
        experience.position = this.experiencePosition.value;
        experience.company = this.experienceCompany.value;
        experience.from = new Date(this.experienceFromYear.value, this.experienceFromMonth.value).toISOString();

        if (this.experienceToMonth.value !== 0) {
            experience.to = new Date(this.experienceToYear.value, this.experienceToMonth.value).toISOString();
        }

        experience.remarks = this.experienceRemarks.join(';');

        this.experience.push(experience);

        this.experiencePosition.setValue('');
        this.experienceCompany.setValue('');
        this.experienceFromMonth.setValue('');
        this.experienceFromYear.setValue('');
        this.experienceToMonth.setValue('');
        this.experienceToYear.setValue('');
        this.experienceRemarks = [];
    }

    removeExperience(index: number): void {
        this.experience.splice(index, 1);
    }

    addExperienceRemark(): void {
        this.experienceRemarks.push(this.experienceRemark.value);
        this.experienceRemark.setValue('');
    }

    removeExperienceRemark(index: number): void {
        this.experienceRemarks.splice(index, 1);
    }

    onExperienceToMonthChanged(value: string): void {
        if (Number.parseInt(value) === 0) {
            this.experienceToYear.disable();
        } else {
            this.experienceToYear.enable();
        }
    }

    onExperienceRemarkChanged(value: string) {
        this.experienceRemark.setValue(value.replace(';', ''))
    }

    addEducation(): void {
        const education = new ResumeEducationForUpdate();
        education.title = this.educationTitle.value;
        education.institution = this.educationInstitution.value;
        education.from = new Date(this.educationFromYear.value, this.educationFromMonth.value).toISOString();

        if (this.educationToMonth.value !== 0) {
            education.to = new Date(this.educationToYear.value, this.educationToMonth.value).toISOString();
        }

        education.remarks = this.educationRemarks.join(';');

        this.education.push(education);

        this.educationTitle.setValue('');
        this.educationInstitution.setValue('');
        this.educationFromMonth.setValue('');
        this.educationFromYear.setValue('');
        this.educationToMonth.setValue('');
        this.educationToYear.setValue('');
        this.educationRemarks = [];
    }

    removeEducation(index: number): void {
        this.education.splice(index, 1);
    }

    addEducationRemark(): void {
        this.educationRemarks.push(this.educationRemark.value);
        this.educationRemark.setValue('');
    }

    removeEducationRemark(index: number): void {
        this.educationRemarks.splice(index, 1);
    }

    onEducationToMonthChanged(value: string): void {
        if (Number.parseInt(value) === 0) {
            this.educationToYear.disable();
        } else {
            this.educationToYear.enable();
        }
    }

    onEducationRemarkChanged(value: string) {
        this.educationRemark.setValue(value.replace(';', ''))
    }

    addCourse(): void {
        const course = new ResumeCourseForUpdate();
        course.name = this.courseName.value;
        course.institution = this.courseInstitution.value;
        course.year = this.courseYear.value;

        this.courses.push(course);

        this.courseName.setValue('');
        this.courseInstitution.setValue('');
        this.courseYear.setValue(new Date().getFullYear());
    }

    removeCourse(index: number): void {
        this.courses.splice(index, 1);
    }

    addSkill(): void {
        const skill = new ResumeSkillForUpdate();
        skill.name = this.skillName.value;

        this.skills.push(skill);

        this.skillName.setValue('');
    }

    removeSkill(index: number): void {
        this.skills.splice(index, 1);
    }

    addLanguage(): void {
        const language = new ResumeLanguageForUpdate();
        language.name = this.languageName.value;
        language.level = this.languageLevel.value;

        this.languages.push(language);

        this.languageName.setValue('');
        this.languageLevel.setValue('');
    }

    removeLanguage(index: number): void {
        this.languages.splice(index, 1);
    }

    addInterest(): void {
        const interest = new ResumeInterestForUpdate();
        interest.name = this.interestName.value;

        this.interests.push(interest);

        this.interestName.setValue('');
    }

    removeInterest(index: number): void {
        this.interests.splice(index, 1);
    }

    private buildResumeForm() {
        this.resumeForm = this.formBuilder.group({
            name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
            title: new FormControl('', [Validators.required, Validators.maxLength(50)]),
            summary: new FormControl('', [Validators.required, Validators.maxLength(600)]),
            phone: new FormControl('', [Validators.maxLength(15)]),
            email: new FormControl('', [Validators.maxLength(100)]),
            website: new FormControl('', [Validators.maxLength(100)]),
            location: new FormControl('', [Validators.maxLength(100)]),
            experiencePosition: new FormControl(''),
            experienceCompany: new FormControl(''),
            experienceFromMonth: new FormControl(1),
            experienceFromYear: new FormControl(new Date().getFullYear()),
            experienceToMonth: new FormControl(1),
            experienceToYear: new FormControl(new Date().getFullYear()),
            experienceRemark: new FormControl(''),
            educationTitle: new FormControl(''),
            educationInstitution: new FormControl(''),
            educationFromMonth: new FormControl(1),
            educationFromYear: new FormControl(new Date().getFullYear()),
            educationToMonth: new FormControl(1),
            educationToYear: new FormControl(new Date().getFullYear()),
            educationRemark: new FormControl(''),
            courseName: new FormControl(''),
            courseInstitution: new FormControl(''),
            courseYear: new FormControl(new Date().getFullYear()),
            skillName: new FormControl(''),
            languageName: new FormControl(''),
            languageLevel: new FormControl(''),
            interestName: new FormControl('')
        });
    }

    private getResumeFromForm(): ResumeForUpdate {
        const resume = new ResumeForUpdate();
        resume.name = this.name.value;
        resume.title = this.title.value;
        resume.summary = this.summary.value;
        resume.phone = this.phone.value;
        resume.email = this.email.value;
        resume.website = this.website.value;
        resume.location = this.location.value;
        resume.experience = [];
        resume.education = [];
        resume.courses = [];
        resume.skills = [];
        resume.languages = [];
        resume.interests = [];

        this.experience.forEach(item => {
            const ex = new ResumeExperienceForUpdate();
            ex.position = item.position;
            ex.company = item.company;
            ex.from = item.from;
            ex.to = item.to;
            ex.remarks = item.remarks;

            resume.experience.push(ex);
        });

        this.education.forEach(item => {
            const ed = new ResumeEducationForUpdate();
            ed.title = item.title;
            ed.institution = item.institution;
            ed.from = item.from;
            ed.to = item.to;
            ed.remarks = item.remarks;

            resume.education.push(ed);
        });

        this.courses.forEach(item => {
            const c = new ResumeCourseForUpdate();
            c.name = item.name;
            c.institution = item.institution;
            c.year = item.year;

            resume.courses.push(c);
        });

        this.skills.forEach(item => {
            const s = new ResumeSkillForUpdate();
            s.name = item.name;

            resume.skills.push(s);
        });

        this.languages.forEach(item => {
            const l = new ResumeLanguageForUpdate();
            l.name = item.name;
            l.level = item.level;

            resume.languages.push(l);
        });

        this.interests.forEach(item => {
            const i = new ResumeSkillForUpdate();
            i.name = item.name;

            resume.interests.push(i);
        });

        return resume;
    }

    private setFormFromResume(resume: Resume): void {
        this.name.setValue(resume.name);
        this.title.setValue(resume.title);
        this.summary.setValue(resume.summary);
        this.phone.setValue(resume.phone);
        this.email.setValue(resume.email);
        this.website.setValue(resume.website);
        this.location.setValue(resume.location);
        this.experience = resume.experience;
        this.education = resume.education;
        this.courses = resume.courses;
        this.skills = resume.skills;
        this.languages = resume.languages;
        this.interests = resume.interests;
    }

    private setMonths(): void {
        this.monthsFrom = Array.from(Array(12), (e, i) => {
            return { id: i + 1, name: new Date(25e8 * ++i).toLocaleString('en-US', { month: 'long' }) }
        });

        this.monthsTo = [...this.monthsFrom];
        this.monthsTo.unshift({ id: 0, name: 'Present' });
    }

    get name() {
        return this.resumeForm.get('name');
    }

    get title() {
        return this.resumeForm.get('title');
    }

    get summary() {
        return this.resumeForm.get('summary');
    }

    get phone() {
        return this.resumeForm.get('phone');
    }

    get email() {
        return this.resumeForm.get('email');
    }

    get website() {
        return this.resumeForm.get('website');
    }

    get location() {
        return this.resumeForm.get('location');
    }

    get experiencePosition() {
        return this.resumeForm.get('experiencePosition');
    }

    get experienceCompany() {
        return this.resumeForm.get('experienceCompany');
    }

    get experienceFromMonth() {
        return this.resumeForm.get('experienceFromMonth');
    }

    get experienceFromYear() {
        return this.resumeForm.get('experienceFromYear');
    }

    get experienceToMonth() {
        return this.resumeForm.get('experienceToMonth');
    }

    get experienceToYear() {
        return this.resumeForm.get('experienceToYear');
    }

    get experienceRemark() {
        return this.resumeForm.get('experienceRemark');
    }

    get educationTitle() {
        return this.resumeForm.get('educationTitle');
    }

    get educationInstitution() {
        return this.resumeForm.get('educationInstitution');
    }

    get educationFromMonth() {
        return this.resumeForm.get('educationFromMonth');
    }

    get educationFromYear() {
        return this.resumeForm.get('educationFromYear');
    }

    get educationToMonth() {
        return this.resumeForm.get('educationToMonth');
    }

    get educationToYear() {
        return this.resumeForm.get('educationToYear');
    }

    get educationRemark() {
        return this.resumeForm.get('educationRemark');
    }

    get courseName() {
        return this.resumeForm.get('courseName');
    }

    get courseInstitution() {
        return this.resumeForm.get('courseInstitution');
    }

    get courseYear() {
        return this.resumeForm.get('courseYear');
    }

    get skillName() {
        return this.resumeForm.get('skillName');
    }

    get languageName() {
        return this.resumeForm.get('languageName');
    }

    get languageLevel() {
        return this.resumeForm.get('languageLevel');
    }

    get interestName() {
        return this.resumeForm.get('interestName');
    }
}