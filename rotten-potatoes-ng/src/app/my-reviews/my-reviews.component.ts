import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { MatDialog } from '@angular/material/dialog';
import { DetailsDialogComponent } from '../details/details-dialog.component';
import { AddDialogComponent } from '../add/add-dialog.component';
import { Review } from '../models/Revew';
import { UserService } from '../services/user.service';

@Component({
    selector: 'my-reviews',
    styleUrls: ['./my-reviews.component.scss'],
    templateUrl: './my-reviews.component.html',
})
export class MyReviewsComponent {

    isLoading: boolean = false;
    reviews: Review[] = [];

    constructor(
        private _userService: UserService
    ) {

    }

    refresh(): void {
        this.isLoading = true;
        this._userService.getReviews()
            .subscribe(o => {
                this.reviews.push(...o);
                this.isLoading = false;
            });
    }

    onReviewDeleted(review: Review): void {
        const i = this.reviews.findIndex(o => o.reviewId === review.reviewId);
        this.reviews.splice(i);
    }
}
