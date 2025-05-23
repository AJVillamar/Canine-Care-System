import { PersonModel } from "./person-model";

export interface OwnerModel extends PersonModel {
    phone?: string
    address?: string
}