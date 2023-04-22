import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ENDPOINTS_MAP } from '../shared/constants/endpoints-config';
import {
    IFilteredQuestions,
    IPostQuestion,
    IQuestion,
    IQuestionDetails,
} from '../shared/models/question';
import { IAnswer, IPostAnswer } from '../shared/models/answer';
import { IQuestionFilters } from '../shared/models/filters';
import { ActivatedRoute, Params, Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})
export class QuestionService {
    constructor(
        private http: HttpClient,
        private router: Router,
        private activatedRoute: ActivatedRoute,
    ) {}

    //for testing purposes only
    getAllQuestions() {
        return this.http.get<IQuestion[]>(
            environment.apiUrl + ENDPOINTS_MAP.QUESTIONS.GET_ALL_QUESTIONS,
        );
    }

    getUsersQuestions(): Observable<IQuestion[]> {
        return this.http.get<IQuestion[]>(
            environment.apiUrl +
                ENDPOINTS_MAP.QUESTIONS.GET_LOGGED_IN_USERS_QUESTIONS,
        );
    }

    getPaginatedAndFilteredQuestions(
        filters: IQuestionFilters,
        pageNumber: number,
    ): Observable<IFilteredQuestions> {
        const queryParams: Params = {
            page: pageNumber,
        };

        // populate queryParams with non-null values and replace null values from filters with empty values
        for (let key of Object.entries(filters)) {
            if (key[1]) {
                queryParams[key[0]] = key[1];
            } else {
                filters[key[0]] = '';
            }
        }

        // basically applies the query params
        this.router.navigate([], {
            relativeTo: this.activatedRoute,
            queryParams: queryParams,
        });

        return this.http.post<IFilteredQuestions>(
            environment.apiUrl +
                ENDPOINTS_MAP.QUESTIONS.FILTER_QUESTIONS +
                (pageNumber - 1),
            filters,
        );
    }

    getQuestionDetails(id: number): Observable<IQuestionDetails> {
        return this.http.get<IQuestionDetails>(
            environment.apiUrl +
                ENDPOINTS_MAP.QUESTIONS.GET_QUESTION_DETAILS +
                id,
        );
    }

    postQuestion(questionToAdd: IPostQuestion): Observable<boolean> {
        return this.http.post<boolean>(
            environment.apiUrl + ENDPOINTS_MAP.QUESTIONS.POST_QUESTION,
            questionToAdd,
        );
    }

    postAnswer(answer: IPostAnswer): Observable<IAnswer> {
        return this.http.post<IAnswer>(
            environment.apiUrl + ENDPOINTS_MAP.ANSWERS.POST_ANSWER,
            answer,
        );
    }

    editAnswer(answer: IPostAnswer): Observable<boolean> {
        return this.http.put<boolean>(
            environment.apiUrl + ENDPOINTS_MAP.ANSWERS.UPDATE_ANSWER,
            answer,
        );
    }

    deleteAnswer(id: number): Observable<boolean> {
        return this.http.delete<boolean>(
            environment.apiUrl + ENDPOINTS_MAP.ANSWERS.DELETE_ANSWER + id,
        );
    }
}
