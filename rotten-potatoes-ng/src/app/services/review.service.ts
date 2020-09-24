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

    public getGame(gameId: number) : Observable<Game> {
        return this._httpClient.get<Game>(`${_baseApiUrl}games?gameId=${gameId}`);
    }

    public getGames(search: string = ''): Observable<Game[]> {
        let url = search.length > 0 ? `${_baseApiUrl}games?search=${search}` : `${_baseApiUrl}games`;
        return this._httpClient.get<Game[]>(url);
    }

    public getReviews(gameId: number): Observable<Review[]> {
        return this._httpClient.get<Review[]>(`${_baseApiUrl}reviews/${gameId}`);
    }

    public putReview(review: Review): Observable<EditReviewResult> {
        return this._httpClient.put<EditReviewResult>(`${_baseApiUrl}reviews`, { GameId: review.gameId, UserId: review.userId, Score: review.score, Details: review.details });
    }

    public deleteReview(reviewId: number) {
        return this._httpClient.delete(`${_baseApiUrl}reviews/${reviewId}`);
    }
}
