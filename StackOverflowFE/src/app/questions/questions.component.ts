import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
    selector: 'app-questions',
    templateUrl: './questions.component.html',
    styleUrls: ['./questions.component.scss'],
})
export class QuestionsComponent implements OnInit {
    form!: FormGroup;

    constructor() {}

    ngOnInit(): void {
        this.form = new FormGroup({
            text: new FormControl(null),
            author: new FormControl(null),
            tag: new FormControl(null),
        });
    }
}
