import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Game } from "../models/Game";
import { Observable } from "rxjs";
import { Review } from '../models/Revew';

const _baseApiUrl: string = "https://localhost:5003/api/reviews/";

@Injectable({
    providedIn: 'root'
})
export class ReviewService {

    constructor(
        private _httpClient: HttpClient
    ) {

    }

    public test() {
        return this._httpClient.get(`${_baseApiUrl}test`)
    }

    //public getGame(gameId: number) : Observable<Game> {
    //    return this._httpClient.get<Game>(`${_baseApiUrl}games/${gameId}`);
    //}

    public getGames(search: string): Observable<Game[]> {
        return this._httpClient.get<Game[]>(`${_baseApiUrl}games/${search}`);
    }

    public getReviews(gameId: number): Observable<Review[]> {
        return this._httpClient.get<Review[]>(`${_baseApiUrl}reviews/${gameId}`);
    }

    public putReview(review: Review) {
        return this._httpClient.put(`${_baseApiUrl}reviews`, { Game: review.game, User: review.user, Score: review.score, Details: review.details });
    }
}
