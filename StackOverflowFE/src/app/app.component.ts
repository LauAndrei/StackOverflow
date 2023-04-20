import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
    title = 'StackOverflow';

    constructor(private accountService: AccountService) {}

    ngOnInit(): void {
        const token = localStorage.getItem('token');
        this.accountService.loadLoggedInUser(token).subscribe(
            () => {},
            () => {
                // if the token expired => the call will fail => remove it from local storage
                // so the user will be redirected to log in
                localStorage.removeItem('token');
            },
        );
    }
}
