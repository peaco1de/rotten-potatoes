import { Component, OnInit, Input, Inject } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { Review } from '../models/Revew';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'details-dialog',
    styleUrls: ['./details-dialog.component.scss'],
    templateUrl: './details-dialog.component.html',
})
export class DetailsDialogComponent implements OnInit {

    isLoading: boolean = false;

    reviews: Review[] = [];

    constructor(
        @Inject(MAT_DIALOG_DATA) public game: Game,
        private _reviewService: ReviewService
    ) {

    }

    ngOnInit(): void {
        this.isLoading = true;
        this._reviewService.getReviews(this.game.id)
            .subscribe(o => {
                this.reviews.push(...o);
                this.isLoading = false;
            });
    }
}
