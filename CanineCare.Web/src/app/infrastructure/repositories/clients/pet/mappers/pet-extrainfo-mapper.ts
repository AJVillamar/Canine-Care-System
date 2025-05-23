import { Mapper } from "@infrastructure/base/mapper";
import { PetExtraInfoEntity } from "../entities/pet-extrainfo-entity";
import { PetExtraInfoModel } from "@domain/models/pet/pet-extrainfo-model";

export class PetExtraInfoMapper extends Mapper<PetExtraInfoModel, PetExtraInfoEntity> {

    override mapToCreate(param: PetExtraInfoModel): PetExtraInfoEntity {
        return {
            allergies: param.allergies,
            preExistingConditions: param.preExistingConditions,
            specialCareInstructions: param.specialCareInstructions,
            feedingNotes: param.feedingNotes
        }
    }

    override mapToUpdate(param: PetExtraInfoModel): PetExtraInfoEntity {
        throw new Error("Method not implemented.");
    }

    override mapFromGet(param: PetExtraInfoEntity): PetExtraInfoModel {
        return {
            id: param.id,
            allergies: param.allergies,
            preExistingConditions: param.preExistingConditions,
            specialCareInstructions: param.specialCareInstructions,
            feedingNotes: param.feedingNotes
        }
    }

}