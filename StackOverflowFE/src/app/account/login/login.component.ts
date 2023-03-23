import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RESPONSE } from 'src/app/shared/constants/response';
import { AccountService } from '../account.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
    loginForm!: FormGroup;
    formSubmitted: boolean = false;

    constructor(
        private accountService: AccountService,
        private router: Router,
        private toastrService: ToastrService,
    ) {}

    ngOnInit(): void {
        this.loginForm = new FormGroup({
            emailOrUsername: new FormControl(null, Validators.required),
            password: new FormControl(null, Validators.required),
        });
    }

    onSubmit() {
        this.formSubmitted = true;
        if (this.loginForm.valid) {
            this.accountService.login(this.loginForm.value).subscribe(
                () => {
                    this.router.navigateByUrl('/home');
                    this.toastrService.success(RESPONSE.SUCCESS);
                },
                (err) => {
                    console.log(err);
                    this.toastrService.error(RESPONSE.ERROR, 'coi', {
                        timeOut: 3000,
                    });
                },
            );
        }
    }
}
