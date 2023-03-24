import { NgModule } from '@angular/core';
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
        path: ':id/:slug',
        component: QuestionDetailsComponent,
    },
];

@NgModule({
    declarations: [],
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class QuestionsRoutingModule {}
