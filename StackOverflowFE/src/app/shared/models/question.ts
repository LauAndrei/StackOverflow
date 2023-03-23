export interface IQuestion {
    id: number;
    authorUsername: string;
    title: string;
    slug: string;
    text: string;
    creationDate: Date;
    lastModifiedDate: Date;
}
