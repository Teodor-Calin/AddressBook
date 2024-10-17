import { AddressDetailsModel } from './AddressDetailsModel';

export interface ContactDetailsModel {
  name: string;
  addresses: AddressDetailsModel[];
}
