import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { ServicePackage } from '../models/service-packages/service-package.model';
import { ServicePackagesService } from '../services/service-packages.service';

@Component({
    selector: 'app-service-packages',
    templateUrl: 'service-packages.component.html'
})
export class ServicePackagesComponent implements OnInit {

    dtOptions: DataTables.Settings = {
        lengthChange: false,
        order: [0, 'desc']
    };

    packages: ServicePackage[];
    packagesTrigger: Subject<any> = new Subject<any>();

    constructor(private servicePackagesService: ServicePackagesService) {

    }

    ngOnInit(): void {
        this.servicePackagesService.getAll()
            .subscribe(packages => {
                this.packages = packages;
                this.packagesTrigger.next();
            });
    }

    ngOnDestroy(): void {
        this.packagesTrigger.unsubscribe();
    }
}