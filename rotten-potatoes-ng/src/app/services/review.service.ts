import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Game } from "../models/Game";
import { Observable } from "rxjs";

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

    public getGame(gameId: number) : Observable<Game> {
        return this._httpClient.get<Game>(`${_baseApiUrl}games/${gameId}`);
    }

}
