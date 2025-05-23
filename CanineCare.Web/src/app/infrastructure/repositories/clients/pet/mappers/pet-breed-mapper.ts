import { PetBreedEntity } from "../entities/pet-breed-entity";
import { PetBreedModel } from "@domain/models/pet/pet-breed-model";

export class PetBreedMapper {

    mapFromGet(param: PetBreedEntity): PetBreedModel {
        return {
            id: param.id,
            name: param.name,
            specie: param.specie
        }
    }

}
