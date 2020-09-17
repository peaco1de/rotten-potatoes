import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';

@Component({
    selector: 'app-games',
    styleUrls: ['./games.component.scss'],
    templateUrl: './games.component.html',
})
export class GamesComponent implements OnInit {

    search: string;

    constructor(
        private _reviewService: ReviewService
    ) {

    }

    ngOnInit(): void {

    }
}
