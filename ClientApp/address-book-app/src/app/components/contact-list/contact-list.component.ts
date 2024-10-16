import { Component, OnInit, ViewChild } from '@angular/core';
import { ContactService } from '../../services/contact.service';
import { CommonModule } from '@angular/common';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { RouterLink, RouterModule } from '@angular/router';

@Component({
  selector: 'app-contact-list',
  standalone: true,
  imports: [CommonModule, MatPaginatorModule, MatButtonModule, MatTableModule, RouterLink, RouterModule],
  templateUrl: './contact-list.component.html',
  styleUrl: './contact-list.component.scss'
})
export class ContactListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'action'];
  dataSource = new MatTableDataSource<any>([]);
  totalContacts = 0;
  pageSize = 5;
  currentPage = 0;

  constructor(private contactService: ContactService) { }

  ngOnInit(): void {
    this.loadContacts(this.currentPage, this.pageSize);
  }

  loadContacts(page: number, size: number): void {
    this.contactService.getContacts(page, size).subscribe(data => {
      this.dataSource.data = data.contacts;
      this.totalContacts = data.total;
    });
  }

  onPageChange(event: PageEvent): void {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadContacts(this.currentPage, this.pageSize);
  }

  viewContact(id: number): void {
    // Navigate to the contact detail page
    console.log(`Viewing contact with ID: ${id}`);
  }

}
