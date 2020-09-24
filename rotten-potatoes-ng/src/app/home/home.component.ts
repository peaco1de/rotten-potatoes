import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { User } from '../models/User';
import { UserService } from '../services/user.service';

@Component({
    selector: 'app-home',
    styleUrls: ['./home.component.scss'],
    templateUrl: './home.component.html',
})
export class HomeComponent{

    constructor(
    ) {

    }

}
