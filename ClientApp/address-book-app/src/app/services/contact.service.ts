// src/app/services/contact.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ContactListItemModel } from '../models/ContactListItemModel';
import { ContactDetailsModel } from '../models/ContactDetailsModel';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  private apiUrl = 'https://localhost:7021/api/contacts';  // Replace with your backend API URL

  constructor(private http: HttpClient) {}

  // Fetch contacts from the backend
  getContacts(pageIndex: number, pageSize: number): Observable<any> {
    const params = {
      page: pageIndex.toString(),
      size: pageSize.toString(),
    };
    return this.http.get<any>(this.apiUrl, { params });
  }

  getContactById(id: number): Observable<ContactDetailsModel> {
    return this.http.get<ContactDetailsModel>(`${this.apiUrl}/${id}`);
  }
}
