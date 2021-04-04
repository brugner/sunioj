import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/auth/user.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser$: Observable<User>;

    constructor(
        private httpClient: HttpClient,
        private router: Router) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')));
        this.currentUser$ = this.currentUserSubject.asObservable();
    }

    public get user(): User {
        return this.currentUserSubject.value;
    }

    login(email: string, password: string): Observable<User> {
        return this.httpClient.post<User>("auth/login", { email, password })
            .pipe(map(user => {
                localStorage.setItem('user', JSON.stringify(user));
                this.currentUserSubject.next(user);
                return user;
            }));
    }

    logout(): void {
        localStorage.removeItem('user');
        this.currentUserSubject.next(null);
        this.router.navigate(['/']);
    }
}