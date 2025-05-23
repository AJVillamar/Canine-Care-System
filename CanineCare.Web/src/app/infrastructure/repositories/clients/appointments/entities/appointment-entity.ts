import { PetEntity } from "../../pet/entities/pet-entity"
import { ServiceEntity } from "../../services/entities/service-entity"
import { ProfessionalEntity } from "@infrastructure/repositories/people/professional/entities/professional-entity"

export interface AppointmentEntity {
    id?: string
    petId?: string
    pet?: PetEntity
    serviceId?: string
    service?: ServiceEntity
    date?: Date
    time?: string
    professionalId?: string
    professional?: ProfessionalEntity
    reason?: string
    status?: string
}