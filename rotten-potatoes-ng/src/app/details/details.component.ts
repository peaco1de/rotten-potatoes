import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';

@Component({
    selector: 'app-details',
    styleUrls: ['./details.component.scss'],
    templateUrl: './details.component.html',
})
export class DetailsComponent implements OnInit {


    isLoading: boolean = false;

    constructor(
        private _reviewService: ReviewService
    ) {

    }

    ngOnInit(): void {

    }
}
