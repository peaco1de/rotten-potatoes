import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';

@Component({
    selector: 'app-games',
    styleUrls: ['./games.component.scss'],
    templateUrl: './games.component.html',
})
export class GamesComponent implements OnInit {


    isLoading: boolean = false;

    games: Game[] = [];
    filteredGames: Game[] = [];

    private _search: string = '';
    get search(): string {
        return this._search;
    }
    set search(search: string) {
        this.isLoading = true;
        this._search = search;
        this.filteredGames = this.games.filter(o => o.name.toLowerCase().includes(search.toLowerCase()));
        this.isLoading = false;
    }

    constructor(
        private _reviewService: ReviewService
    ) {

    }

    ngOnInit(): void {
        this.isLoading = true;
        this._reviewService.getScores()
            .subscribe(o => {
                this.games.push(...o);
                this.filteredGames.push(...this.games);
                this.isLoading = false;
            });
    }
}
