import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileComponent } from './profile.component';
import { ProfileRoutingModule } from './profile-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { QuestionsAskedComponent } from './questions-asked/questions-asked.component';
import { QuestionsModule } from '../questions/questions.module';

@NgModule({
    declarations: [
        ProfileComponent,
        DashboardComponent,
        QuestionsAskedComponent,
    ],
    imports: [CommonModule, ProfileRoutingModule, QuestionsModule],
})
export class ProfileModule {}
