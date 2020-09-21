import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { FlexLayoutModule } from '@angular/flex-layout'

import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { GamesComponent } from './games/games.component';
import { DetailsDialogComponent } from './details/details-dialog.component';
import { AddDialogComponent } from './add/add-dialog.component';
import { MatFormFieldModule } from '@angular/material/form-field';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        GamesComponent,
        DetailsDialogComponent,
        AddDialogComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
        ]),
        BrowserAnimationsModule,

        FlexLayoutModule,

        //material
        MatTabsModule,
        MatInputModule,
        MatIconModule,
        MatButtonModule,
        MatListModule,
        MatProgressSpinnerModule,
        MatCardModule,
        MatDialogModule,
        MatFormFieldModule,
        MatSelectModule,
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
