import { Mapper } from "@infrastructure/base/mapper";
import { OwnerEntity } from "../entities/owner-entity";
import { OwnerModel } from "@domain/models/people/owner-model";

export class OwnerMapper extends Mapper<OwnerModel, OwnerEntity> {

    override mapToCreate(param: OwnerModel): OwnerEntity {
        return {
            identification: param.identification,
            firstName: param.firstName,
            lastName: param.lastName,
            email: param.email,
            phone: param.phone,
            address: param.address
        }
    } 

    override mapToUpdate(param: OwnerModel): OwnerEntity {
        return {
            id: param.id,
            firstName: param.firstName,
            lastName: param.lastName,
            email: param.email,
            phone: param.phone,
            address: param.address
        }
    }

    override mapFromGet(param: OwnerEntity): OwnerModel {
        return {
            id: param.id,
            identification: param.identification,
            firstName: param.firstName,
            lastName: param.lastName,
            email: param.email,
            phone: param.phone,
            address: param.address        
        }
    }

}
