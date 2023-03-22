import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
    loginForm!: FormGroup;
    formSubmitted: boolean = false;

    constructor() {}

    ngOnInit(): void {
        this.loginForm = new FormGroup({
            emailOrUsername: new FormControl(null, Validators.required),
            password: new FormControl(null, Validators.required),
        });
    }

    onSubmit() {
        this.formSubmitted = true;
        if (this.loginForm.valid) {
            console.log(this.loginForm.value);
        }
    }
}
