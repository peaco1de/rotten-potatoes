import { Component, OnInit, Input, Inject, Output } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { Review } from '../models/Revew';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { User } from '../models/User';
import { UserService } from '../services/user.service';

@Component({
    selector: 'add-dialog',
    styleUrls: ['./add-dialog.component.scss'],
    templateUrl: './add-dialog.component.html',
})
export class AddDialogComponent implements OnInit {

    isLoading: boolean = false;

    newReview: Review;

    constructor(
        @Inject(MAT_DIALOG_DATA) public game: Game,
        private _dialogRef: MatDialogRef<AddDialogComponent>,
        private _reviewService: ReviewService,
        private _userService: UserService
    ) {

    }

    ngOnInit(): void {
        this._userService.getReview(this.game.id)
            .subscribe(o => {
                if (o != null) {
                    this.newReview =
                    {
                        reviewId: o.reviewId,
                        gameId: o.gameId,
                        userId: o.userId,
                        score: o.score,
                        details: o.details,
                        addDate: new Date()
                    };
                }
                else {
                    this.newReview = {
                        reviewId: null,
                        gameId: this.game.id,
                        userId: this._userService.getSelectedUser().userId,
                        score: null,
                        details: "",
                        addDate: new Date()
                    }
                }
            });

    }

    submit() {
        this._reviewService.putReview(this.newReview)
            .subscribe(o => {
                if (o.status === 'Added') {
                    this.game.avgScore = (this.game.avgScore * this.game.numberOfReviews + this.newReview.score) / ++this.game.numberOfReviews;
                } else if (o.status === 'Edited') {
                    this.game.avgScore = (this.game.avgScore * this.game.numberOfReviews + o.scoreChange) / this.game.numberOfReviews;
                }
                this._dialogRef.close(this.newReview);
            });
    }
}
