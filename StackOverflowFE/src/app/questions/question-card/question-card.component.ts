import { Component, Input } from '@angular/core';
import { IQuestion } from 'src/app/shared/models/question';

@Component({
    selector: 'app-question-card',
    templateUrl: './question-card.component.html',
    styleUrls: ['./question-card.component.scss'],
})
export class QuestionCardComponent {
    @Input() question: IQuestion;

    @Input() isMain: boolean = true;
}
