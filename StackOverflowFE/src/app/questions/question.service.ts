import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ENDPOINTS_MAP } from '../shared/constants/endpoints-config';
import { IQuestion, IQuestionDetails } from '../shared/models/question';
import { IAnswer, IPostAnswer } from '../shared/models/answer';

@Injectable({
    providedIn: 'root',
})
export class QuestionService {
    constructor(private http: HttpClient) {}

    //for testing purposes only
    getAllQuestions() {
        return this.http.get<IQuestion[]>(
            environment.apiUrl + ENDPOINTS_MAP.QUESTIONS.GET_ALL_QUESTIONS,
        );
    }

    getQuestionDetails(id: number): Observable<IQuestionDetails> {
        return this.http.get<IQuestionDetails>(
            environment.apiUrl +
                ENDPOINTS_MAP.QUESTIONS.GET_QUESTION_DETAILS +
                id,
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
