import { PetBreedEntity } from "./pet-breed-entity"
import { PetExtraInfoEntity } from "./pet-extrainfo-entity"
import { OwnerEntity } from "@infrastructure/repositories/people/owner/entities/owner-entity"

export interface PetEntity {
    id?: string
    name?: string
    breedId?: string
    breed?: PetBreedEntity
    birthDate?: Date
    sex?: string
    color?: string
    weight?: number
    petExtraInfo?: PetExtraInfoEntity
    ownerId?: string
    owner?: OwnerEntity
}