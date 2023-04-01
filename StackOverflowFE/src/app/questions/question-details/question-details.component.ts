import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IQuestionDetails } from 'src/app/shared/models/question';
import { QuestionService } from '../question.service';
import { ToastrService } from 'ngx-toastr';
import { IPostAnswer } from '../../shared/models/answer';
import { RESPONSE } from '../../shared/constants/response';

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
        private questionService: QuestionService,
        private activatedRoute: ActivatedRoute,
        private toastService: ToastrService,
    ) {}

    ngOnInit(): void {
        this.activatedRoute.paramMap.subscribe((params) => {
            this.questionId = +params.get('id');
        });

        this.questionService.getQuestionDetails(this.questionId).subscribe(
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
            this.questionService.postAnswer(answer).subscribe(
                (addedAnswer) => {
                    this.toastService.success(RESPONSE.ANSWER.SUCCESS_POST);
                    this.questionDetails.answers.push(addedAnswer);
                    this.answerText = '';
                },
                () => {
                    this.toastService.error(RESPONSE.ERROR);
                },
            );
        }
    }

    deleteAnswer(id: number) {
        this.questionService.deleteAnswer(id).subscribe(
            () => {
                this.removeAnswerFromArray(id);
                this.toastService.success(RESPONSE.ANSWER.SUCCESS_DELETE);
            },
            () => {
                this.toastService.error(RESPONSE.ERROR);
            },
        );
    }

    private removeAnswerFromArray(id: number) {
        this.questionDetails.answers = this.questionDetails.answers.filter(
            (x) => x.id !== id,
        );
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
