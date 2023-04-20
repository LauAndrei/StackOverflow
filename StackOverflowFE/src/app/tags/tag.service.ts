import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TagDto } from '../shared/models/tag';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ENDPOINTS_MAP } from '../shared/constants/endpoints-config';

@Injectable({
    providedIn: 'root',
})
export class TagService {
    constructor(private http: HttpClient) {}

    getAllTags(): Observable<TagDto[]> {
        return this.http.get<TagDto[]>(
            environment.apiUrl + ENDPOINTS_MAP.TAGS.GET_ALL_TAGS,
        );
    }

    createTag(tagToCreate: TagDto): Observable<TagDto> {
        return this.http.post<TagDto>(
            environment.apiUrl + ENDPOINTS_MAP.TAGS.POST_TAG,
            tagToCreate,
        );
    }
}
