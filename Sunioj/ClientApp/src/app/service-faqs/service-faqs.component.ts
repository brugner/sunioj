import { ServiceFaqsService } from './../services/service-faqs.service';
import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { ServiceFaq } from '../models/service-faqs/service-faq.model';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ServiceFaqForCreation } from '../models/service-faqs/service-faq-for-creation.model';
import { SettingsService } from '../services/settings.service';

@Component({
    selector: 'app-service-faqs',
    templateUrl: 'service-faqs.component.html'
})
export class ServiceFaqsComponent implements OnInit {

    siteName: string;
    isLoading: boolean = true;
    isSubmitting: boolean = false;
    faqForm: FormGroup;

    dtOptions: DataTables.Settings = {
        lengthChange: false,
        order: [0, 'desc']
    };

    faqs: ServiceFaq[];
    faqsTrigger: Subject<any> = new Subject<any>();

    constructor(
        private serviceFaqsService: ServiceFaqsService,
        private toastrService: ToastrService,
        private formBuilder: FormBuilder,
        private settingsService: SettingsService) {

    }

    async ngOnInit(): Promise<void> {
        this.siteName = await this.settingsService.getSetting('site.name');

        this.buildFaqForm();

        this.serviceFaqsService.getAll()
            .subscribe(faqs => {

                this.faqs = faqs;
                this.faqsTrigger.next();
                this.order.setValue(this.findMaxFaqsOrder() + 1);
                this.isLoading = false;
            });
    }

    ngOnDestroy(): void {
        this.faqsTrigger.unsubscribe();
    }

    deleteFaq(id: number): void {
        if (confirm("Are you sure you want to delete this FAQ?")) {
            this.serviceFaqsService.delete(id).subscribe(() => {
                const index = this.faqs.findIndex(x => x.id === id);
                this.faqs.splice(index, 1);

                this.toastrService.success('FAQ deleted', this.siteName);
            });
        };
    }

    submitForm(): void {
        this.isSubmitting = true;
        const faqForCreation = this.getFaqForCreation();

        this.serviceFaqsService.create(faqForCreation)
            .subscribe((faq: ServiceFaq) => {
                this.isSubmitting = false;
                this.toastrService.success('FAQ created', this.siteName);
                this.faqs.push(faq)
                this.cleanFaqForm();
                this.faqForm.reset();
                this.order.setValue(this.findMaxFaqsOrder() + 1);
            }, () => {
                this.isSubmitting = false;
            });
    }

    private getFaqForCreation(): ServiceFaqForCreation {
        let faq = new ServiceFaqForCreation();
        faq.question = this.question.value;
        faq.answer = this.answer.value;
        faq.order = this.order.value;

        return faq;
    }

    private cleanFaqForm(): void {
        this.question.setValue('');
        this.answer.setValue('');
        this.order.setValue('');
    }

    private buildFaqForm() {
        this.faqForm = this.formBuilder.group({
            question: new FormControl('', [Validators.required, Validators.maxLength(100)]),
            answer: new FormControl('', [Validators.required]),
            order: new FormControl(1, [Validators.required])
        });
    }

    private findMaxFaqsOrder(): number {
        if (!this.faqs || this.faqs.length === 0) {
            return 0;
        }

        let max = this.faqs[0].order;

        for (let i = 1, len = this.faqs.length; i < len; i++) {
            let current = this.faqs[i].order;
            max = (current > max) ? current : max;
        }

        return max;
    }

    get question() {
        return this.faqForm.get('question');
    }

    get answer() {
        return this.faqForm.get('answer');
    }

    get order() {
        return this.faqForm.get('order');
    }
}