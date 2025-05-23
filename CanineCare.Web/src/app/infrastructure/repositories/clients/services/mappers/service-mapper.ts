import { Mapper } from "@infrastructure/base/mapper";
import { ServiceEntity } from "../entities/service-entity";
import { ServiceModel } from "@domain/models/appointments/service-model";
import { ServiceType } from "@domain/models/appointments/service-type-model";

export class ServiceMapper extends Mapper<ServiceModel, ServiceEntity> {

    override mapToCreate(param: ServiceModel): ServiceEntity {
        return {
            name: param.name,
            description: param.description,
            type: param.type,
            actions: param.actions?.map(item => ({
                title: item.title,
                details: item.description
            }))
        }
    }

    override mapToUpdate(param: ServiceModel): ServiceEntity {
        throw new Error("Method not implemented.");
    }

    override mapFromGet(param: ServiceEntity): ServiceModel {
        return {
            id: param.id,
            name: param.name,
            description: param.description,
            type: this.parseServiceType(param.type!),
            actions: param.actions?.map(item => ({
                title: item.title,
                description: item.details
            }))
        }
    }

    private parseServiceType(value: string): ServiceType {
        switch (value.toLowerCase()) {
            case "baño normal":
                return ServiceType.NormalBath;
            case "baño medicado":
                return ServiceType.MedicatedBath;
            case "peluquería canina":
                return ServiceType.Grooming;
            case "desparasitación":
                return ServiceType.Deworming;
            default:
                throw new Error(`Tipo de servicio no reconocido: ${value}`);
        }
    }
    
}