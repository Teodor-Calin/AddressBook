import { ContactListItemModel } from './ContactListItemModel';

export interface ContactListResponseModel {
  contacts: ContactListItemModel[];
  total: number;
}
