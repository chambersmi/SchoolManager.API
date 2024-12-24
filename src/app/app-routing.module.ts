import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StudentListComponent } from './features/students/student-list/student-list.component';

const routes: Routes = [
  {
    path: 'admin/students',
    component: StudentListComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
