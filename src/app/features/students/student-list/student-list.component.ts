import { Component, OnInit } from '@angular/core';
import { Student } from '../models/student.model';
import { Observable } from 'rxjs';
import { StudentService } from '../student.service';
import { Address } from '../models/address.model';

@Component({
  selector: 'app-student-list',
  standalone: false,
  
  templateUrl: './student-list.component.html',
  styleUrl: './student-list.component.css'
})
export class StudentListComponent implements OnInit {
  students$?:Observable<Student[]>;
  addresses$?:Observable<Address[]>;

  constructor(private studentService:StudentService) { }

  ngOnInit(): void {
    this.students$ = this.studentService.getAllStudents();
  }
}
