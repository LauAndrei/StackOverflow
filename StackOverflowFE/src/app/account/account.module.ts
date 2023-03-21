import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountRoutingModule } from './account-routing.module';
import { RouterModule } from '@angular/router';

@NgModule({
    declarations: [LoginComponent, RegisterComponent],
    imports: [CommonModule, AccountRoutingModule, RouterModule],
})
export class AccountModule {}
