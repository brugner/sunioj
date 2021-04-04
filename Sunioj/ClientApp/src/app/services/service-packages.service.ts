import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServicePackage } from '../models/service-packages/service-package.model';
import { ServicePackageForCreation } from '../models/service-packages/service-package-for-creation.model';
import { ServicePackageForUpdate } from '../models/service-packages/service-package-for-update.model';

@Injectable()
export class ServicePackagesService {

    constructor(private httpClient: HttpClient) {

    }

    getAll(): Observable<ServicePackage[]> {
        return this.httpClient.get<ServicePackage[]>('service-packages');
    }

    getById(id: number): Observable<ServicePackage> {
        return this.httpClient.get<ServicePackage>(`service-packages/${id}`);
    }

    delete(id: number) {
        return this.httpClient.delete(`service-packages/${id}`);
    }

    create(servicePackage: ServicePackageForCreation): Observable<ServicePackage> {
        return this.httpClient.post<ServicePackage>('service-packages', servicePackage);
    }

    update(id: number, servicePackage: ServicePackageForUpdate): Observable<ServicePackage> {
        return this.httpClient.put<ServicePackage>(`service-packages/${id}`, servicePackage);
    }
}