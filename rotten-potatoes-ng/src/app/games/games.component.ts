import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { MatDialog } from '@angular/material/dialog';
import { DetailsDialogComponent } from '../details/details-dialog.component';
import { AddDialogComponent } from '../add/add-dialog.component';
import { GameService } from '../services/game.service';

@Component({
    selector: 'app-games',
    styleUrls: ['./games.component.scss'],
    templateUrl: './games.component.html',
})
export class GamesComponent implements OnInit {

    isLoading: boolean = false;

    games: Game[] = [];

    search: string = '';

    constructor(
        private _dialog: MatDialog,
        private _gameService: GameService
    ) {

    }

    ngOnInit(): void {
        this.refresh();
    }

    refresh(): void {
        this.search = '';
        this.filter();
    }

    filter(): void {
        this.isLoading = true;
        this._gameService.getGames(this.search)
            .subscribe(o => {
                this.games = o;
                this.isLoading = false;
            });
    }

    showDetails(game: Game) {
        this._dialog.open(DetailsDialogComponent, {
            data: game
        });
    }

    showAdd(game: Game) {
        this._dialog.open(AddDialogComponent, {
            data: game
        });
    }
}
