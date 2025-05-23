import { PetEntity } from "../entities/pet-entity";
import { PetBreedMapper } from "./pet-breed-mapper";
import { Mapper } from "@infrastructure/base/mapper";
import { PetModel } from "@domain/models/pet/pet-model";
import { PetExtraInfoMapper } from "./pet-extrainfo-mapper";
import { PetBreedEntity } from "../entities/pet-breed-entity";
import { PetBreedModel } from "@domain/models/pet/pet-breed-model";
import { OwnerMapper } from "@infrastructure/repositories/people/owner/mappers/owner-mapper";

export class PetMapper extends Mapper<PetModel, PetEntity> {

    private readonly _ownerMapper = new OwnerMapper();
    private readonly _breedMapper = new PetBreedMapper();
    private readonly _extraInfoMapper = new PetExtraInfoMapper()

    override mapToCreate(param: PetModel): PetEntity {
        return {
            name: param.name,
            breedId: param.breed?.id,
            birthDate: param.birthDate,
            sex: param.sex,
            color: param.color,
            weight: param.weight,
            petExtraInfo: this._extraInfoMapper.mapToCreate(param.extraInfo!),
            ownerId: param.owner?.id
        }
    }

    override mapToUpdate(param: PetModel): PetEntity {
        return {
            id: param.id,
            name: param.name,
            breedId: param.breed?.id,
            birthDate: param.birthDate,
            sex: param.sex,
            color: param.color,
            weight: param.weight,
            petExtraInfo: this._extraInfoMapper.mapToCreate(param.extraInfo!),
        }
    }

    override mapFromGet(param: PetEntity): PetModel {
        return {
            id: param.id,
            name: param.name,
            birthDate: param.birthDate,
            sex: param.sex === 'Macho' || param.sex === 'Hembra' ? param.sex : undefined,
            color: param.color,
            weight: param.weight,
            extraInfo: param.petExtraInfo ? this._extraInfoMapper.mapFromGet(param.petExtraInfo) : undefined,
            owner: param.owner ? this._ownerMapper.mapFromGet(param.owner) : undefined,
            breed: param.breed ? this._breedMapper.mapFromGet(param.breed) : undefined
        };
    }


    mapFromGetBreed(param: PetBreedEntity): PetBreedModel {
        return {
            id: param.id,
            name: param.name,
            specie: param.specie
        }
    }

}