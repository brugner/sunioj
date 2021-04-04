import { Component, OnInit } from '@angular/core';
import { ServiceFaq } from '../models/service-faqs/service-faq.model';
import { ServicePackage } from '../models/service-packages/service-package.model';
import { ServiceFaqsService } from '../services/service-faqs.service';
import { ServicePackagesService } from '../services/service-packages.service';

@Component({
    selector: 'app-services-and-pricing',
    templateUrl: 'services-and-pricing.component.html'
})
export class ServicesAndPricingComponent implements OnInit {

    packages: ServicePackage[] = [];
    faqs: ServiceFaq[] = [];

    constructor(
        private servicePackagesService: ServicePackagesService,
        private serviceFaqsService: ServiceFaqsService) {

    }

    ngOnInit(): void {
        this.servicePackagesService.getAll()
            .subscribe(packages => {
                this.packages = packages;
            });

        this.serviceFaqsService.getAll()
            .subscribe(faqs => {
                this.faqs = faqs;
            });
    }
}