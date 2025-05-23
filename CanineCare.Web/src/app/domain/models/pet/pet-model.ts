import { PetBreedModel } from "./pet-breed-model"
import { OwnerModel } from "../people/owner-model"
import { PetExtraInfoModel } from "./pet-extrainfo-model"

export interface PetModel {
    id?: string
    name?: string
    breed?: PetBreedModel
    birthDate?: Date
    sex?: 'Macho' | 'Hembra'
    color?: string
    weight?: number
    extraInfo?: PetExtraInfoModel
    owner?: OwnerModel
}