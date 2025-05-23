import { PersonModel } from "./person-model";

export interface ProfessionalModel extends PersonModel {
    birthDate?: Date
    yearsOfExperience?: number
} 