import { Component, OnInit, Input, Inject } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { Review } from '../models/Revew';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'create-user',
    styleUrls: ['./create-user.component.scss'],
    templateUrl: './create-user.component.html',
})
export class CreateUserDialogComponent implements OnInit {

    isLoading: boolean = false;

    newReview: Review;

    constructor(
        @Inject(MAT_DIALOG_DATA) public game: Game,
        private _reviewService: ReviewService
    ) {

    }

    ngOnInit(): void {
        this.newReview = {
            game: this.game.id,
            user: "test",
            score: null,
            details: "",
            addDate: new Date()
        };
    }

    submit() {
        this._reviewService.putReview(this.newReview)
            .subscribe();
        this.game.avgScore = (this.game.avgScore * this.game.numberOfReviews + this.newReview.score) / ++this.game.numberOfReviews;
    }
}
