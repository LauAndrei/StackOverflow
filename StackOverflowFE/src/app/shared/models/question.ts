import { IAnswer } from './answer';
import { TagReduced } from './tag';

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
