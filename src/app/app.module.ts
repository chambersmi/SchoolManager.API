import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/navbar/navbar.component';
import { StudentListComponent } from './features/students/student-list/student-list.component';
import { provideHttpClient } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    StudentListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent]
})
export class AppModule { }
