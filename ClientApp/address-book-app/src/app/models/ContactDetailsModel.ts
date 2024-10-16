import { AddressDetailsModel } from "./AddressDetailsModel";

export interface ContactDetailsModel {
  name: string;
  email: string;
  phone: string;
  addresses: AddressDetailsModel[];
}