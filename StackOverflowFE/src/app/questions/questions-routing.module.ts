import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QuestionDetailsComponent } from './question-details/question-details.component';
import { QuestionsComponent } from './questions.component';
import { PostQuestionComponent } from './post-question/post-question.component';

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
    {
        path: 'post-question',
        component: PostQuestionComponent,
    },
];

@NgModule({
    declarations: [],
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class QuestionsRoutingModule {}
