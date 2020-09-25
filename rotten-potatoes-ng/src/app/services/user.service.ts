import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { User } from '../models/User';
import { Review } from '../models/Revew';
import { Game } from '../models/Game';

const _baseApiUrl: string = "https://localhost:5003/api/users/";

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private _selectedUser: User;

    constructor(
        private _httpClient: HttpClient
    ) {

    }

    public setSelectedUser(user: User) {
        this._selectedUser = user;
    }

    public getSelectedUser(): User {
        return this._selectedUser;
    }

    public getUsers(): Observable<User[]> {
        return this._httpClient.get<User[]>(`${_baseApiUrl}`);
    }

    public createUser(userName: string): Observable<User> {
        return this._httpClient.post<User>(`${_baseApiUrl}`, { UserName: userName });
    }

    public getReview(gameId: number, userId: number = this._selectedUser.userId): Observable<Review> {
        return this._httpClient.get<Review>(`${_baseApiUrl}${userId}/reviews/${gameId}`);
    }

    public getReviews(userId: number = this._selectedUser.userId): Observable<Review[]> {
        return this._httpClient.get<Review[]>(`${_baseApiUrl}${userId}/reviews`);
    }

    public addFavorite(gameId: number, userId: number = this._selectedUser.userId) {
        return this._httpClient.post(`${_baseApiUrl}${userId}/favorites`, { GameId: gameId });
    }

    public removeFavorite(gameId: number, userId: number = this._selectedUser.userId) {
        return this._httpClient.delete(`${_baseApiUrl}${userId}/favorites/${gameId}`);
    }

    public getFavoriteGames(search: string, userId: number = this._selectedUser.userId): Observable<Game[]> {
        return this._httpClient.get<Game[]>([`${_baseApiUrl}${userId}/games`, (search.length > 0 ? `?search=${search}` : '')].join(''));
    }
}
