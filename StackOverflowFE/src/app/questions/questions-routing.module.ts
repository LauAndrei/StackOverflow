import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { QuestionDetailsComponent } from './question-details/question-details.component';
import { QuestionsComponent } from './questions.component';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        component: QuestionsComponent,
    },
    {
        path: 'question-details/:id/:slug',
        component: QuestionDetailsComponent,
    },
];

@NgModule({
    declarations: [],
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class QuestionsRoutingModule {}
