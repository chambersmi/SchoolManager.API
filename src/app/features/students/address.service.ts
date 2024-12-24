import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Address } from './models/address.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AddressService implements OnInit {

  constructor(private httpClient:HttpClient) { }
  ngOnInit(): void {          
    }

    getAllAddresses():Observable<Address[]> {
      return this.httpClient.get<Address[]>(`${environment.apibaseUrl}/api/Student`);
    }
  }