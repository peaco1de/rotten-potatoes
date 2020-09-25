import { Component, OnInit, ViewChild } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { User } from '../models/User';
import { UserService } from '../services/user.service';
import { GamesComponent } from '../games/games.component';
import { MyReviewsComponent } from '../my-reviews/my-reviews.component';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { FavoritesComponent } from '../favorites/favorites.component';



@Component({
    selector: 'app-home',
    styleUrls: ['./home.component.scss'],
    templateUrl: './home.component.html',
})
export class HomeComponent {

    @ViewChild(GamesComponent)
    private _gamesComponent: GamesComponent;
    @ViewChild(FavoritesComponent)
    private _favoritesComponent: FavoritesComponent;
    @ViewChild(MyReviewsComponent)
    private _myReviewsComponent: MyReviewsComponent;

    constructor(
    ) {

    }

    onTabChanged(event: MatTabChangeEvent) {
        switch (event.index) {
            case 0: {
                this._gamesComponent.refresh();
                break;
            }
            case 1: {
                this._favoritesComponent.refresh();
                break;
            }
            case 2: {
                this._myReviewsComponent.refresh();
                break;
            }
        }
    }
}
