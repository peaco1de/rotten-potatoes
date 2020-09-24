import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { MatDialog } from '@angular/material/dialog';
import { DetailsDialogComponent } from '../details/details-dialog.component';
import { AddDialogComponent } from '../add/add-dialog.component';
import { Review } from '../models/Revew';
import { UserService } from '../services/user.service';

@Component({
    selector: 'my-review',
    styleUrls: ['./my-review.component.scss'],
    templateUrl: './my-review.component.html',
})
export class MyReviewComponent implements OnInit {

    isLoading: boolean = false;

    @Input()
    review: Review;

    @Output()
    reviewDeleted: EventEmitter<Review> = new EventEmitter<Review>();

    game: Game;

    constructor(
        private _dialog: MatDialog,
        private _reviewService: ReviewService
    ) {

    }

    ngOnInit(): void {
        this._reviewService.getGame(this.review.gameId)
            .subscribe(o => this.game = o[0]);
    }

    showEdit() {
        const dialogRef = this._dialog.open(AddDialogComponent, {
            data: this.game
        });

        dialogRef.afterClosed().subscribe(
            o => {
                if (o != '') this.review = o;
            }
        );
    }

    delete() {
        this._reviewService.deleteReview(this.review.reviewId)
            .subscribe(o => this.reviewDeleted.emit(this.review));
    }
}
