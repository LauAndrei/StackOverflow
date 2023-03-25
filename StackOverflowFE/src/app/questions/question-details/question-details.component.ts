import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IQuestionDetails } from 'src/app/shared/models/question';
import { QuestionsService } from '../questions.service';

@Component({
    selector: 'app-question-details',
    templateUrl: './question-details.component.html',
    styleUrls: ['./question-details.component.scss'],
})
export class QuestionDetailsComponent implements OnInit {
    questionId: number;
    questionDetails: IQuestionDetails;
    isLoading: boolean = true;

    constructor(
        private questionsService: QuestionsService,
        private activatedRoute: ActivatedRoute,
    ) {}

    ngOnInit(): void {
        this.activatedRoute.paramMap.subscribe((params) => {
            this.questionId = +params.get('id');
        });
        this.questionsService.getQuestionDetails(this.questionId).subscribe(
            (question) => {
                this.questionDetails = question;
            },
            () => {},
            () => {
                this.isLoading = false;
            },
        );
    }
}
