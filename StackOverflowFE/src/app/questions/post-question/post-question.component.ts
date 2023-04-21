import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TagDto } from '../../shared/models/tag';
import { TagService } from '../../tags/tag.service';
import { ToastrService } from 'ngx-toastr';
import { RESPONSE } from '../../shared/constants/response';
import { QuestionService } from '../question.service';
import { IPostQuestion } from '../../shared/models/question';

@Component({
    selector: 'app-post-question',
    templateUrl: './post-question.component.html',
    styleUrls: ['./post-question.component.scss'],
})
export class PostQuestionComponent implements OnInit {
    form: FormGroup;
    questionTags: TagDto[] = [];
    existingTags: TagDto[] = [];
    currentTag: string;

    formSubmitted = false;

    constructor(
        private tagService: TagService,
        private toastrService: ToastrService,
        private questionService: QuestionService,
    ) {}

    ngOnInit(): void {
        this.form = new FormGroup({
            title: new FormControl('', Validators.required),
            text: new FormControl('', Validators.required),
        });

        // for testing purposes only -- later will get the tags using a request
        // made after inserting 2 letters
        this.tagService.getAllTags().subscribe((tags) => {
            this.existingTags = tags;
        });
    }

    addTag() {
        if (!this.isTagOk()) {
            return;
        }

        const tagToAdd = this.findTag();

        if (!tagToAdd) {
            this.toastrService.error(
                'You can create it with "Create Tag" button!',
                RESPONSE.TAG.NOT_FOUND,
            );
            return;
        }

        this.questionTags.push(tagToAdd);
        this.currentTag = null;
        console.log(this.questionTags);
    }

    createTag() {
        if (!this.isTagOk()) {
            return;
        }

        const tagToCreate: TagDto = { id: 0, name: this.currentTag };

        this.tagService.createTag(tagToCreate).subscribe(
            (tag) => {
                this.existingTags.push(tag);
                this.toastrService.success(RESPONSE.TAG.SUCCESS_POST);
            },
            () => {
                this.toastrService.error(RESPONSE.ERROR);
            },
        );
    }

    removeTag(position: number) {
        this.questionTags.splice(position, 1);
        console.log(this.questionTags);
    }

    postQuestion() {
        if (!this.form.valid) {
            return;
        }

        let questionToAdd: IPostQuestion = { ...this.form.value };
        questionToAdd.tags = this.questionTags;

        this.questionService.postQuestion(questionToAdd).subscribe(
            () => {
                this.toastrService.success(RESPONSE.QUESTION.SUCCESS_POST);
                this.form.reset();
            },
            () => {
                this.toastrService.error(RESPONSE.ERROR);
            },
        );
    }

    private findTag() {
        return this.existingTags.find((tag) => tag.name == this.currentTag);
    }

    private isTagOk(): boolean {
        if (!this.currentTag) {
            this.toastrService.error('Tag cannot be empty!');
            return false;
        }

        if (this.currentTag?.includes(' ')) {
            this.toastrService.error('Tags cannot contain spaces!');
            return false;
        }

        return true;
    }
}
