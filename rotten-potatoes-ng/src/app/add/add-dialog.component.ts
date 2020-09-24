import { Component, OnInit, Input, Inject } from '@angular/core';
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
        private _reviewService: ReviewService,
        private _userService: UserService
    ) {

    }

    ngOnInit(): void {

        this.newReview = {
            game: this.game.id,
            userName: this._userService.getSelectedUser().userName,
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
