import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestionsComponent } from './questions.component';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';
import { QuestionCardComponent } from './question-card/question-card.component';
import { QuestionDetailsComponent } from './question-details/question-details.component';
import { QuestionsRoutingModule } from './questions-routing.module';

@NgModule({
    declarations: [
        QuestionsComponent,
        QuestionCardComponent,
        QuestionDetailsComponent,
    ],
    imports: [
        CommonModule,
        SharedModule,
        ReactiveFormsModule,
        QuestionsRoutingModule,
    ],
})
export class QuestionsModule {}
