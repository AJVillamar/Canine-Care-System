import { PetModel } from "../pet/pet-model"
import { ServiceModel } from "./service-model"
import { ProfessionalModel } from "../people/professional-model"

export interface AppointmentModel {
    id?: string
    pet?: PetModel
    service?: ServiceModel
    date?: Date
    time?: string
    professional?: ProfessionalModel
    reason?: string
    status?: string
}