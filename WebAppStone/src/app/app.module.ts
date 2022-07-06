import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from "@angular/common/http";
import { AppRoutingModule } from './routes/app-routing.module';
import { AppComponent } from './app.component';
import { PersonComponent } from './pages/person/person.component';
import {MatDialogModule, MatDialogRef} from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatInputModule} from '@angular/material/input';
import { ModalPersonComponent } from './shared/components/modal-person/modal-person.component';
import { ReactiveFormsModule } from '@angular/forms';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatSelectModule} from '@angular/material/select';
import { FormsModule } from '@angular/forms';
import { TreeComponent } from './shared/components/tree-view/tree-view.component';
import {MatIconModule} from '@angular/material/icon';
import { NavBarComponent } from './shared/components/navbar/navbar.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import {MatPaginatorModule} from '@angular/material/paginator';

@NgModule({
  declarations: [
    AppComponent,
    PersonComponent,
    ModalPersonComponent,
    TreeComponent,
    NavBarComponent,
    DashboardComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatSelectModule,
    BrowserModule,
    AppRoutingModule,    
    HttpClientModule,
    MatDialogModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatSnackBarModule,
    MatIconModule,
    NgxChartsModule,
    MatPaginatorModule
  ],
  providers: [
    {
      provide: MatDialogRef,
      useValue: {}
    },
    
 ],
  bootstrap: [AppComponent]
})
export class AppModule { }
