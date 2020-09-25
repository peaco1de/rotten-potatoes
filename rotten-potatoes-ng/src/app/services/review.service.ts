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

    public putReview(review: Review): Observable<EditReviewResult> {
        return this._httpClient.put<EditReviewResult>(`${_baseApiUrl}`, { GameId: review.gameId, UserId: review.userId, Score: review.score, Details: review.details });
    }

    public deleteReview(reviewId: number) {
        return this._httpClient.delete(`${_baseApiUrl}${reviewId}`);
    }
}
