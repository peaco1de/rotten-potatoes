import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { MatDialog } from '@angular/material/dialog';
import { DetailsDialogComponent } from '../details/details-dialog.component';
import { AddDialogComponent } from '../add/add-dialog.component';
import { GameService } from '../services/game.service';
import { UserService } from '../services/user.service';
import { User } from '../models/User';

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
        private _gameService: GameService,
        private _userService: UserService,
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
        this._gameService.getGames(this.search, this._userService.getSelectedUser().userId)
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

    toggleFavorite(game: Game) {
        if (game.isFavorite) {
            this._userService.removeFavorite(game.id)
                .subscribe(() => game.isFavorite = false);
        } else {
            this._userService.addFavorite(game.id)
                .subscribe(() => game.isFavorite = true);
        }
    }
}
