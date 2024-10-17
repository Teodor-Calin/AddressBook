import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ContactDetailsModel } from '../models/ContactDetailsModel';
import { ContactListResponseModel } from '../models/ContactListResponseModel';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  private apiUrl = 'https://localhost:7021/api/contacts';

  constructor(private http: HttpClient) {}

  getContacts(
    pageIndex: number,
    pageSize: number
  ): Observable<ContactListResponseModel> {
    const params = {
      page: `${pageIndex}`,
      size: `${pageSize}`,
    };
    return this.http.get<ContactListResponseModel>(this.apiUrl, { params });
  }

  getContactById(id: number): Observable<ContactDetailsModel> {
    return this.http.get<ContactDetailsModel>(`${this.apiUrl}/${id}`);
  }
}
