import { IAnswer } from './answer';
import { TagDto, TagReduced } from './tag';

export interface IQuestion {
    id: number;
    authorUsername: string;
    title: string;
    slug: string;
    text: string;
    creationDate: Date;
    lastModifiedDate: Date;
}

export interface IQuestionDetails {
    title: string;
    text: string;
    authorUsername: string;
    pictureUrl: string;
    datePosted: Date;
    lastModifiedDate: Date;
    answers: IAnswer[];
    tags: TagReduced[];
    score: number;
}

export interface IPostQuestion {
    id: number;
    title: string;
    text: string;
    pictureUrl: string;
    tags: TagDto[];
}

export interface IFilteredQuestions {
    questions: IQuestion[];
    totalNumberOfQuestions: number;
}
