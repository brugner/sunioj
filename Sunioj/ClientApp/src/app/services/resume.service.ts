import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Resume } from '../models/resume/resume.model';
import { ResumeForUpdate } from '../models/resume/resume-for-update.model';

@Injectable()
export class ResumeService {

    constructor(private httpClient: HttpClient) {

    }

    get(): Observable<Resume> {
        return this.httpClient.get<Resume>('resumes');
    }

    update(resumeForUpdate: ResumeForUpdate): Observable<Resume> {
        return this.httpClient.put<Resume>('resumes', resumeForUpdate);
    }
}