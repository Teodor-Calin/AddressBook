import { RouterModule, Routes } from '@angular/router';
import { ContactListComponent } from './components/contact-list/contact-list.component';
import { ContactDetailsComponent } from './components/contact-details/contact-details.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { LayoutComponent } from './components/layout/layout.component';

export const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
          { path: '', component: ContactListComponent },
          { path: 'contact/:id', component: ContactDetailsComponent },
          { path: '404', component: NotFoundComponent },
        ]
      }
];