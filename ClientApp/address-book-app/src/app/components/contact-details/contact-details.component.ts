import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ContactService } from '../../services/contact.service'; // Adjust the path as needed
import { AddressDetailsModel } from '../../models/AddressDetailsModel';
import { AddressTypes, addressTypesList } from '../../enums/AddressTypes';
import { catchError, of } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-contact-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './contact-details.component.html',
  styleUrls: ['./contact-details.component.scss']
})
export class ContactDetailsComponent implements OnInit{
  contact: any;
  loading: boolean = true;
  error: string | undefined = undefined;

  constructor(private route: ActivatedRoute, private contactService: ContactService, private router: Router) {
  }

  ngOnInit(): void {
    this.fetchContactDetails();
  }

  fetchContactDetails(): void {
    const contactId = this.route.snapshot.paramMap.get('id'); // Get the contact ID from the route
    if (contactId) {
      this.contactService.getContactById(Number(contactId)).pipe(
        catchError((err: HttpErrorResponse) => {
          if (err.status == 404) {
          this.error = 'Could not fetch contact details.';
          this.loading = false;
          this.router.navigate(['/404']);
          return of(null);
          }
          return of(null);
        })
      ).subscribe(data => {
        if (data) {
          this.contact = data;
          this.loading = false;
        }
      });
      
      //   (data) => {
      //     this.contact = data;
      //     this.loading = false;
      //   },
      //   (err) => {
      //     this.error = 'Could not fetch contact details.';
      //     this.loading = false;
      //     //this.router.navigate(['/404'])
      //   }
      // );
    } 
    // else {
    //   this.error = 'Contact ID is missing.';
    //   this.loading = false;
    // }
  }

  get addressesByType(): { [key: number]: AddressDetailsModel[] } {
    return this.contact.addresses.reduce((acc: any, address: { addressType: number; }) => {
      if (!acc[address.addressType]) {
        acc[address.addressType] = [];
      }
      acc[address.addressType].push(address);
      return acc;
    }, {} as { [key: number]: AddressDetailsModel[] });
  }

  getAddressTypeName(type: string): string | undefined {
    return addressTypesList.find(t => t.id == Number(type))?.name
  }
}
