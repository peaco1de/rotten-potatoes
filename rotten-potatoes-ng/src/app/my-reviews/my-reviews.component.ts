import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { MatDialog } from '@angular/material/dialog';
import { DetailsDialogComponent } from '../details/details-dialog.component';
import { AddDialogComponent } from '../add/add-dialog.component';
import { Review } from '../models/Revew';

@Component({
    selector: 'my-reviews',
    styleUrls: ['./my-reviews.component.scss'],
    templateUrl: './my-reviews.component.html',
})
export class MyReviewsComponent implements OnInit {

    isLoading: boolean = false;
    reviews: Review[];

    constructor(
        private _dialog: MatDialog,
        private _reviewService: ReviewService
    ) {

    }

    ngOnInit(): void {
        this.isLoading = true;
        this._reviewService.getGames(this.search)
            .subscribe(o => {
                this.games.push(...o);
                this.filteredGames.push(...this.games);
                this.isLoading = false;
            });
    }

    showAdd(game: Game) {
        this._dialog.open(AddDialogComponent, {
            data: game
        });
    }
}
