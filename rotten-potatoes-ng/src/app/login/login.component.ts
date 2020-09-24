import { Component, OnInit } from '@angular/core';
import { ReviewService } from '../services/review.service';
import { UserService } from '../services/user.service';
import { User } from '../models/User';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { CreateUserDialogComponent } from '../create-user/create-user.component';

@Component({
    selector: 'app-login',
    styleUrls: ['./login.component.scss'],
    templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

    users: User[] = [];
    selectedUser: User;

    constructor(
        private _router: Router,
        private _userService: UserService,
        private _dialog: MatDialog
    ) {

    }

    ngOnInit(): void {
        this._userService.getUsers()
            .subscribe(o =>
                this.users.push(...o));
    }

    createUser() {
        const dialogRef = this._dialog.open(CreateUserDialogComponent);
        dialogRef.afterClosed().subscribe(
            o => {
                if (o) { this.users.push(o); }
            }
        );
    }

    submit() {
        if (this.selectedUser != null) {
            this._userService.setSelectedUser(this.selectedUser);
            this._router.navigate(['home']);
        }
    }
}
