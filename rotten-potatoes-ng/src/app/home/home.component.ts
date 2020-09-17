import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

    game: Game;

    constructor(
        private _reviewService: ReviewService
    ) {

    }

    ngOnInit(): void {
        //this._reviewService.getGame(2155)
        //    .subscribe(o => this.game = o);
    }
}
