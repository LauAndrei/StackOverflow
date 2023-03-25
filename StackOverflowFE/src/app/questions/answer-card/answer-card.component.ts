import { Component, Input } from '@angular/core';
import { IAnswer } from '../../shared/models/answer';

@Component({
    selector: 'app-answer-card',
    templateUrl: './answer-card.component.html',
    styleUrls: ['./answer-card.component.scss'],
})
export class AnswerCardComponent {
    @Input() answer: IAnswer;

    constructor() {}
}
