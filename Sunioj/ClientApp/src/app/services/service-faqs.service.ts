import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServiceFaq } from '../models/service-faqs/service-faq.model';
import { ServiceFaqForCreation } from '../models/service-faqs/service-faq-for-creation.model';

@Injectable()
export class ServiceFaqsService {

    constructor(private httpClient: HttpClient) {

    }

    getAll(): Observable<ServiceFaq[]> {
        return this.httpClient.get<ServiceFaq[]>('service-faqs');
    }

    create(serviceFaq: ServiceFaqForCreation): Observable<ServiceFaq> {
        return this.httpClient.post<ServiceFaq>('service-faqs', serviceFaq);
    }

    delete(id: number) {
        return this.httpClient.delete(`service-faqs/${id}`);
    }
}