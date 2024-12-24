import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Student } from './models/student.model';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http:HttpClient) {
    //constructor
   }

   getAllStudents():Observable<Student[]> {
    return this.http.get<Student[]>(`${environment.apibaseUrl}/api/Student`);    
   }
}
