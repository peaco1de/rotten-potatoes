import { Component, OnInit, Input, Inject } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { Review } from '../models/Revew';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { AddDialogComponent } from '../add/add-dialog.component';

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
        private _dialog: MatDialog,
        private _reviewService: ReviewService
    ) {

    }

    ngOnInit(): void {
        this.isLoading = true;
        console.log(this.game);
        this._reviewService.getReviews(this.game.id)
            .subscribe(o => {
                this.reviews.push(...o);
                this.isLoading = false;
            });
    }

    showAdd(game: Game) {
        this._dialog.open(AddDialogComponent, {
            data: game
        });
    }
}
