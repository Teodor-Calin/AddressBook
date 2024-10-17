import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactService } from '../../services/contact.service'; // Adjust the path as needed
import { AddressDetailsModel } from '../../models/AddressDetailsModel';
import { addressTypes } from '../../enums/AddressTypes';
import { catchError, of } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { ContactDetailsModel } from '../../models/ContactDetailsModel';

@Component({
  selector: 'app-contact-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact-details.component.html',
  styleUrls: ['./contact-details.component.scss'],
})
export class ContactDetailsComponent implements OnInit {
  contact: ContactDetailsModel | null = null;
  loading: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private contactService: ContactService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.fetchContactDetails();
  }

  fetchContactDetails(): void {
    const contactId = this.route.snapshot.paramMap.get('id');

    if (!contactId) {
      return;
    }

    this.contactService
      .getContactById(Number(contactId))
      .pipe(
        catchError((err: HttpErrorResponse) => {
          if (err.status == 404) {
            this.loading = false;
            this.router.navigate(['/404']);
            return of(null);
          }
          return of(null);
        })
      )
      .subscribe((data) => {
        if (data) {
          this.contact = data;
          this.loading = false;
        }
      });
  }

  get addressesByType(): { [key: number]: AddressDetailsModel[] } {
    if (!this.contact) {
      return {};
    }

    return this.contact.addresses.reduce(
      (
        acc: { [key: number]: AddressDetailsModel[] },
        address: AddressDetailsModel
      ) => {
        if (!acc[address.addressType]) {
          acc[address.addressType] = [];
        }
        acc[address.addressType].push(address);
        return acc;
      },
      {} as { [key: number]: AddressDetailsModel[] }
    );
  }

  getAddressTypeName(type: string): string | undefined {
    return addressTypes[type];
  }
}
