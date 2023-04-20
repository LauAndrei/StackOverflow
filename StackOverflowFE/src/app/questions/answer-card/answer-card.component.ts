import {
    Component,
    EventEmitter,
    Input,
    OnDestroy,
    OnInit,
    Output,
} from '@angular/core';
import { IAnswer, IPostAnswer } from '../../shared/models/answer';
import { Subscription } from 'rxjs';
import { ILoggedInUser } from '../../shared/models/user';
import { AccountService } from '../../account/account.service';
import { QuestionService } from '../question.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RESPONSE } from '../../shared/constants/response';

@Component({
    selector: 'app-answer-card',
    templateUrl: './answer-card.component.html',
    styleUrls: ['./answer-card.component.scss'],
})
export class AnswerCardComponent implements OnInit, OnDestroy {
    @Input() answer: IAnswer;
    @Output() delete: EventEmitter<number> = new EventEmitter<number>();
    currentUser: ILoggedInUser;
    private currentUserSub: Subscription;
    showIcons: boolean = false;
    editMode: boolean = false;
    answerEditedText: string = '';
    questionId: number;

    constructor(
        private accountService: AccountService,
        private questionService: QuestionService,
        private activatedRoute: ActivatedRoute,
        private toastService: ToastrService,
    ) {}

    ngOnInit(): void {
        this.activatedRoute.paramMap.subscribe((params) => {
            this.questionId = +params.get('id');
        });

        this.currentUserSub = this.accountService.currentUser$.subscribe(
            (user) => {
                this.currentUser = user;
            },
        );
    }

    ngOnDestroy(): void {
        this.currentUserSub.unsubscribe();
    }

    onMouseEnter() {
        if (this.answer.authorUsername === this.currentUser.userName) {
            this.showIcons = true;
        }
    }

    onMouseLeave() {
        this.showIcons = false;
    }

    onEdit() {
        this.editMode = !this.editMode;
        this.answerEditedText = this.answer.text;
    }

    onSave() {
        const copyAnswer: IPostAnswer = {
            id: this.answer.id,
            pictureUrl: '',
            questionId: this.questionId,
            text: this.answerEditedText,
        };

        this.questionService.editAnswer(copyAnswer).subscribe(
            (res) => {
                this.editMode = false;
                this.answer.text = this.answerEditedText;
                this.toastService.success(RESPONSE.ANSWER.SUCCESS_EDIT);
            },
            () => {
                this.toastService.error(RESPONSE.ERROR);
            },
        );
    }

    onDelete() {
        this.delete.emit(this.answer.id);
    }
}
