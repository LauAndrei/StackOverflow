import { Component, OnInit } from '@angular/core';
import { TagService } from './tag.service';
import { TagDto } from '../shared/models/tag';

@Component({
    selector: 'app-tags',
    templateUrl: './tags.component.html',
    styleUrls: ['./tags.component.scss'],
})
export class TagsComponent implements OnInit {
    tags: TagDto[] = [];

    constructor(private tagService: TagService) {}

    ngOnInit(): void {
        this.tagService.getAllTags().subscribe((tags) => {
            this.tags = tags;
        });
    }
}
