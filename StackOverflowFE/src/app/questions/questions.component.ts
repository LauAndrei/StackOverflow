import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IQuestion } from '../shared/models/question';
import { QuestionService } from './question.service';
import { ActivatedRoute, Route } from '@angular/router';

@Component({
    selector: 'app-questions',
    templateUrl: './questions.component.html',
    styleUrls: ['./questions.component.scss'],
})
export class QuestionsComponent implements OnInit {
    form!: FormGroup;
    questions: IQuestion[] = [];
    totalNumberOfQuestions: number = 0;

    filters: {} = {};

    currentPage: number = 1;

    //number of question per page, hardcoded at the moment
    pageSize: number = 6;

    isLoading: boolean = true;

    constructor(
        private questionsService: QuestionService,
        private activatedRoute: ActivatedRoute,
    ) {}

    ngOnInit(): void {
        this.form = new FormGroup({
            title: new FormControl(null),
            authorUsername: new FormControl(null),
            tag: new FormControl(null),
        });

        this.activatedRoute.queryParams.subscribe(({ page, ...params }) => {
            this.filters = { ...params };
            this.currentPage = page ? +page : 1;
            this.setFormValue();
        });

        this.questionsService
            .getPaginatedAndFilteredQuestions(
                { ...this.form.value },
                this.currentPage,
            )
            .subscribe(
                (result) => {
                    this.questions = result.questions;
                    this.totalNumberOfQuestions = result.totalNumberOfQuestions;
                },
                (err) => {
                    console.log(err);
                },
                () => {
                    this.isLoading = false;
                },
            );
    }

    filterQuestions() {
        if (this.form.dirty) {
            this.isLoading = true;
            this.questionsService
                .getPaginatedAndFilteredQuestions(
                    { ...this.form.value },
                    this.currentPage,
                )
                .subscribe(
                    (result) => {
                        this.questions = result.questions;
                        this.totalNumberOfQuestions =
                            result.totalNumberOfQuestions;
                    },
                    (err) => {
                        console.log(err);
                    },
                    () => {
                        this.isLoading = false;
                    },
                );

            this.form.reset({
                ...this.form.value,
            });
        }
    }

    changePage(pageNumber: number) {
        this.questionsService
            .getPaginatedAndFilteredQuestions(
                { ...this.form.value },
                pageNumber,
            )
            .subscribe((result) => {
                this.questions = result.questions;
                this.currentPage = pageNumber;
            });
    }

    private setFormValue() {
        this.form.patchValue({ ...this.filters });
        console.log(this.form.value);
    }
}
