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
        this.accountService.loadLoggedInUser(token).subscribe({
            next: () => console.log('loaded user'),
            error: () => console.log('error'),
        });
    }
}
