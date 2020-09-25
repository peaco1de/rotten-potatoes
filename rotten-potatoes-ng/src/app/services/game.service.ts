import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Game } from '../models/Game';
import { Review } from '../models/Revew';

const _baseApiUrl: string = "https://localhost:5003/api/games/";

@Injectable({
    providedIn: 'root'
})
export class GameService {

    constructor(
        private _httpClient: HttpClient
    ) {

    }

    public getGame(gameId: number): Observable<Game> {
        return this._httpClient.get<Game>(`${_baseApiUrl}?gameId=${gameId}`);
    }

    public getGames(search: string = ''): Observable<Game[]> {
        let url = search.length > 0 ? `${_baseApiUrl}?search=${search}` : `${_baseApiUrl}games`;
        return this._httpClient.get<Game[]>(url);
    }

    public getReviews(gameId: number): Observable<Review[]> {
        return this._httpClient.get<Review[]>(`${_baseApiUrl}/${gameId}/reviews`);
    }
}
