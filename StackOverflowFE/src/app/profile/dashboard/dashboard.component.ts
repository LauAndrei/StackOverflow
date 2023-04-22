import {Component, OnInit} from '@angular/core';
import {AccountService} from '../../account/account.service';
import {Observable} from 'rxjs';
import {ILoggedInUser} from '../../shared/models/user';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
    currentUser$: Observable<ILoggedInUser>;

    constructor(private accountService: AccountService) {
    }

    ngOnInit(): void {
        this.currentUser$ = this.accountService.currentUser$;
    }

    logOut() {
        if (!confirm("Do you want to log out?")) {
            return;
        }

        this.accountService.logOut();
    }
}
