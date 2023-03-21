import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountComponent } from './account.component';

const routes: Routes = [
    {
        path: '',
        component: AccountComponent,
        children: [
            {
                path: 'login',
                component: LoginComponent,
            },
            { path: 'register', component: RegisterComponent },
            {
                path: '**',
                redirectTo: 'login',
            },
        ],
    },
];

@NgModule({
    declarations: [],
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AccountRoutingModule {}
