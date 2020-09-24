import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Game } from "../models/Game";
import { Observable } from "rxjs";
import { Review } from '../models/Revew';
import { EditReviewResult } from '../models/EditReviewResult';

const _baseApiUrl: string = "https://localhost:5003/api/reviews/";

@Injectable({
    providedIn: 'root'
})
export class ReviewService {

    constructor(
        private _httpClient: HttpClient
    ) {

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

    public putReview(review: Review): Observable<EditReviewResult> {
        console.log(review);
        return this._httpClient.put<EditReviewResult>(`${_baseApiUrl}reviews`, { GameId: review.gameId, UserId: review.userId, Score: review.score, Details: review.details });
    }
}
