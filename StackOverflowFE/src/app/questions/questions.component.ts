import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IQuestion } from '../shared/models/question';
import { QuestionService } from './question.service';

@Component({
    selector: 'app-questions',
    templateUrl: './questions.component.html',
    styleUrls: ['./questions.component.scss'],
})
export class QuestionsComponent implements OnInit {
    form!: FormGroup;
    questions: IQuestion[];

    constructor(private questionsService: QuestionService) {}

    ngOnInit(): void {
        this.form = new FormGroup({
            text: new FormControl(null),
            author: new FormControl(null),
            tag: new FormControl(null),
        });

        this.questionsService.getAllQuestions().subscribe(
            (questions) => {
                this.questions = questions;
            },
            (err) => {
                console.log(err);
            },
        );
    }
}
