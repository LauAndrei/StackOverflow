export interface IAnswer {
    id: number;
    authorUsername: string;
    text: string;
    datePosted: Date;
    lastModifiedDate: Date;
    pictureUrl: string;
    score: number;
}

export interface IPostAnswer {
    id: number;
    text: string;
    questionId: number;
    pictureUrl: string;
}
