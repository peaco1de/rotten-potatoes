import { EventEmitter, Component, OnInit, Input, Inject, Output } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { Game } from '../models/Game';
import { Review } from '../models/Revew';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { User } from '../models/User';
import { UserService } from '../services/user.service';

@Component({
    selector: 'create-user',
    styleUrls: ['./create-user.component.scss'],
    templateUrl: './create-user.component.html',
})
export class CreateUserDialogComponent implements OnInit {

    isSaving: boolean = false;

    userName: string;

    constructor(
        public dialogRef: MatDialogRef<CreateUserDialogComponent>,
        private _userService: UserService
    ) {

    }

    ngOnInit(): void {
    }

    submit() {
        if (this.userName.length > 0) {
            this._userService.createUser(this.userName)
                .subscribe(o => {
                    if (o != null) {
                        this.dialogRef.close(o);
                    }
                    else {
                        this.dialogRef.close();
                    }
                });
        }
    }
}
