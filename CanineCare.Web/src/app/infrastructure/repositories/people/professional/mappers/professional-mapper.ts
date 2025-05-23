import { Mapper } from "@infrastructure/base/mapper";
import { ProfessionalEntity } from "../entities/professional-entity";
import { ProfessionalModel } from "@domain/models/people/professional-model";

export class ProfessionalMapper extends Mapper<ProfessionalModel, ProfessionalEntity> {

    override mapToCreate(param: ProfessionalModel): ProfessionalEntity {
        return {
            identification: param.identification,
            firstName: param.firstName,
            lastName: param.lastName,
            email: param.email,
            birthDate: param.birthDate,
            yearsOfExperience: param.yearsOfExperience
        }
    }

    override mapToUpdate(param: ProfessionalModel): ProfessionalEntity {
        throw new Error("Method not implemented.");
    }

    override mapFromGet(param: ProfessionalEntity): ProfessionalModel {
        return {
            id: param.id,
            identification: param.identification,
            firstName: param.firstName,
            lastName: param.lastName,
            email: param.email,
            birthDate: param.birthDate,
            yearsOfExperience: param.yearsOfExperience
        }
    }

}
