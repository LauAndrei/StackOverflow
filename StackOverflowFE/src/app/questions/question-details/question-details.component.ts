import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IQuestionDetails } from 'src/app/shared/models/question';
import { QuestionsService } from '../questions.service';
import { ToastrService } from 'ngx-toastr';
import { IPostAnswer } from '../../shared/models/answer';
import { RESPONSE } from '../../shared/constants/response';
import { async, Observable } from 'rxjs';
import { ILoggedInUser } from '../../shared/models/user';
import { AccountService } from '../../account/account.service';

@Component({
    selector: 'app-question-details',
    templateUrl: './question-details.component.html',
    styleUrls: ['./question-details.component.scss'],
})
export class QuestionDetailsComponent implements OnInit {
    questionId: number;
    questionDetails: IQuestionDetails;
    isLoading: boolean = true;
    answerText: string = '';

    constructor(
        private questionsService: QuestionsService,
        private activatedRoute: ActivatedRoute,
        private toastrService: ToastrService,
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

    postAnswer() {
        if (!this.isEmptyOrSpaces(this.answerText)) {
            const answer = this.createAnswer(this.answerText);
            this.questionsService.postAnswer(answer).subscribe(
                (addedAnswer) => {
                    this.toastrService.success(RESPONSE.ANSWER.SUCCESS_POST);
                    this.questionDetails.answers.push(addedAnswer);
                },
                (err) => {
                    this.toastrService.error(RESPONSE.ERROR);
                },
            );
        }
    }

    private isEmptyOrSpaces(text: string) {
        return text === null || text.match(/^ *$/) !== null;
    }

    private createAnswer(text: string): IPostAnswer {
        return {
            id: 0,
            pictureUrl: '',
            questionId: this.questionId,
            text: text,
        };
    }
}
