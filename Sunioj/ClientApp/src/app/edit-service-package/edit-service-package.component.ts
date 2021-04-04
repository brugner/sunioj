import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ServicePackageForCreation } from '../models/service-packages/service-package-for-creation.model';
import { ServicePackageForUpdate } from '../models/service-packages/service-package-for-update.model';
import { ServicePackage } from '../models/service-packages/service-package.model';
import { ServicePackagesService } from '../services/service-packages.service';
import { SettingsService } from '../services/settings.service';

@Component({
    selector: 'app-edit-service-package',
    templateUrl: 'edit-service-package.component.html'
})
export class EditServicePackageComponent implements OnInit {

    siteName: string;
    isLoading: boolean = true;
    isNew: boolean = false;
    isSubmitting: boolean = false;
    servicePackageForm: FormGroup;

    constructor(
        private servicePackagesService: ServicePackagesService,
        private route: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder,
        private toastrService: ToastrService,
        private settingsService: SettingsService) {

    }

    async ngOnInit(): Promise<void> {
        this.siteName = await this.settingsService.getSetting('site.name');

        this.buildServicePackageForm();

        const id = Number.parseInt(this.route.snapshot.paramMap.get('id'));

        if (isNaN(id)) {
            this.isLoading = false;
            this.isNew = true;
            this.cleanServicePackageForm();
        }
        else {
            this.loadServicePackage(id);
        }
    }

    submitForm(): void {
        this.isSubmitting = true;

        if (this.isNew) {
            const servicePackage = this.getServicePackageForCreation();

            this.servicePackagesService.create(servicePackage)
                .subscribe(() => {
                    this.isSubmitting = false;
                    this.toastrService.success('Post created', this.siteName);
                    this.cleanServicePackageForm();
                    this.router.navigateByUrl('/service-packages');
                }, () => {
                    this.isSubmitting = false;
                });
        }
        else {
            const servicePackage = this.getServicePackageForUpdate();

            this.servicePackagesService.update(this.id.value, servicePackage)
                .subscribe(() => {
                    this.isSubmitting = false;
                    this.toastrService.success('Post updated', this.siteName);
                    this.cleanServicePackageForm();
                    this.router.navigateByUrl('/service-packages');
                }, () => {
                    this.isSubmitting = false;
                });
        }
    }

    deleteServicePackage(): void {
        if (confirm("Are you sure you want to delete this service package?")) {
            this.servicePackagesService.delete(this.id.value).subscribe(() => {
                this.toastrService.success('Service package deleted', this.siteName);
                this.router.navigateByUrl('/service-packages')
            });
        };
    }

    private getServicePackageForCreation(): ServicePackageForCreation {
        let servicePackage = new ServicePackageForCreation();
        servicePackage.name = this.name.value;
        servicePackage.description = this.description.value;
        servicePackage.price = this.price.value;
        servicePackage.order = this.order.value;

        return servicePackage;
    }

    private getServicePackageForUpdate(): ServicePackageForUpdate {
        let servicePackage = new ServicePackageForUpdate();
        servicePackage.name = this.name.value;
        servicePackage.description = this.description.value;
        servicePackage.price = this.price.value;
        servicePackage.order = this.order.value;

        return servicePackage;
    }

    private cleanServicePackageForm(): void {
        this.id.setValue('');
        this.name.setValue('');
        this.description.setValue('');
        this.price.setValue('');
        this.order.setValue('');
    }

    private buildServicePackageForm() {
        this.servicePackageForm = this.formBuilder.group({
            id: new FormControl(''),
            name: new FormControl('', [Validators.required, Validators.maxLength(50)]),
            description: new FormControl('', [Validators.required]),
            price: new FormControl(0, [Validators.required]),
            order: new FormControl(1, [Validators.required])
        });
    }

    private loadServicePackage(id: number) {
        this.servicePackagesService.getById(id)
            .subscribe(servicePackage => {
                this.setFormFromServicePackage(servicePackage);
                this.isLoading = false;
            });
    }

    private setFormFromServicePackage(servicePackage: ServicePackage) {
        this.id.setValue(servicePackage.id);
        this.name.setValue(servicePackage.name);
        this.description.setValue(servicePackage.description);
        this.price.setValue(servicePackage.price);
        this.order.setValue(servicePackage.order);
    }

    get id() {
        return this.servicePackageForm.get('id');
    }

    get name() {
        return this.servicePackageForm.get('name');
    }

    get description() {
        return this.servicePackageForm.get('description');
    }

    get price() {
        return this.servicePackageForm.get('price');
    }

    get order() {
        return this.servicePackageForm.get('order');
    }
}