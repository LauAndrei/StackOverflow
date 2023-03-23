import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ENDPOINTS_MAP } from '../shared/constants/endpoints-config';
import { IQuestion } from '../shared/models/question';

@Injectable({
    providedIn: 'root',
})
export class QuestionsService {
    constructor(private http: HttpClient) {}

    //for testing purposes only
    getAllQuestions() {
        return this.http.get<IQuestion[]>(
            environment.apiUrl + ENDPOINTS_MAP.QUESTIONS.GET_ALL_QUESTIONS,
        );
    }
}
