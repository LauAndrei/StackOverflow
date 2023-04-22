import { Component, OnInit } from '@angular/core';
import { IQuestion } from '../../shared/models/question';
import { QuestionService } from '../../questions/question.service';

@Component({
    selector: 'app-questions-asked',
    templateUrl: './questions-asked.component.html',
    styleUrls: ['./questions-asked.component.scss'],
})
export class QuestionsAskedComponent implements OnInit {
    questionCards: IQuestion[] = [];

    constructor(private questionService: QuestionService) {}

    ngOnInit() {
        this.questionService.getUsersQuestions().subscribe((questions) => {
            this.questionCards = questions;
        });
    }
}
