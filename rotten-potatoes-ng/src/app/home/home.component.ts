import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';

@Component({
    selector: 'app-home',
    styleUrls: ['./home.component.scss'],
    templateUrl: './home.component.html',
})
export class HomeComponent {

    game: Game;

    constructor(
    ) {

    }

    ngOnInit(): void {
    }
}
