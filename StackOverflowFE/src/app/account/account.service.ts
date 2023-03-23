import { Injectable } from '@angular/core';
import { map, of, ReplaySubject } from 'rxjs';
import { ILoggedInUser } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { ENDPOINTS_MAP } from '../shared/constants/endpoints-config';

@Injectable({
    providedIn: 'root',
})
export class AccountService {
    private currentUserSource = new ReplaySubject<ILoggedInUser | null>(1);
    currentUser$ = this.currentUserSource.asObservable();

    constructor(private http: HttpClient, private router: Router) {}

    login(values: any) {
        return this.http
            .post<ILoggedInUser>(ENDPOINTS_MAP.AUTHENTICATION.LOGIN, values)
            .pipe(
                map((user: ILoggedInUser) => {
                    if (user) {
                        localStorage.setItem('token', user.token);
                        this.currentUserSource.next(user);
                    }
                }),
            );
    }

    register(values: any) {
        return this.http
            .post<ILoggedInUser>(ENDPOINTS_MAP.AUTHENTICATION.REGISTER, values)
            .pipe(
                map((user: ILoggedInUser) => {
                    if (user) {
                        localStorage.setItem('token', user.token);
                        this.currentUserSource.next(user);
                    }
                }),
            );
    }

    logOut() {
        localStorage.removeItem('token');
        this.currentUserSource.next(null);
        this.router.navigateByUrl('/login');
    }

    loadLoggedInUser(token: string | null) {
        if (token === null) {
            this.currentUserSource.next(null);
            return of(null);
        }

        let headers = new HttpHeaders();
        headers = headers.set('Authorization', `Bearer ${token}`);
        return this.http
            .get<ILoggedInUser>(ENDPOINTS_MAP.AUTHENTICATION.GET_CURRENT_USER, {
                headers,
            })
            .pipe(
                map((user: ILoggedInUser) => {
                    localStorage.setItem('token', user.token);
                    this.currentUserSource.next(user);
                }),
            );
    }
}
