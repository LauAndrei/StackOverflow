import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionsComponent } from './questions.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { QuestionCardComponent } from './question-card/question-card.component';
import { QuestionDetailsComponent } from './question-details/question-details.component';
import { QuestionsRoutingModule } from './questions-routing.module';
import { AnswerCardComponent } from './answer-card/answer-card.component';

@NgModule({
    declarations: [
        QuestionsComponent,
        QuestionCardComponent,
        QuestionDetailsComponent,
        AnswerCardComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        ReactiveFormsModule,
        QuestionsRoutingModule,
        FormsModule,
    ],
})
export class QuestionsModule {}
