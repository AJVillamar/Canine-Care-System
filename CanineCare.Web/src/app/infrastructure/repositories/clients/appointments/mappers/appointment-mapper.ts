import { PetMapper } from "../../pet/mappers/pet-mapper";
import { AppointmentEntity } from "../entities/appointment-entity";
import { ServiceMapper } from "../../services/mappers/service-mapper";
import { AppointmentHourEntity } from "../entities/appointment-hour-entity";
import { AppointmentSearchFilterEntity } from "../entities/appointment-searchfilter-entity";

import { Mapper } from "@infrastructure/base/mapper";
import { ProfessionalMapper } from "@infrastructure/repositories/people/professional/mappers/professional-mapper";

import { AppointmentModel } from "@domain/models/appointments/appointment-model";
import { AppointmentHourModel } from "@domain/models/appointments/appointment-hour-model";
import { AppointmentSearchFilterModel } from "@domain/models/appointments/appointment-searchfilter-model";

export class AppointmentMapper extends Mapper<AppointmentModel, AppointmentEntity> {

    private readonly _serviceMapper = new ServiceMapper();
    private readonly _professionalMapper = new ProfessionalMapper();
    private readonly _petMapper = new PetMapper();

    override mapToCreate(param: AppointmentModel): AppointmentEntity {
        return {
            petId: param.pet?.id,
            serviceId: param.service?.id,
            date: param.date,
            time: param.time,
            professionalId: param.professional?.id,
            reason: param.reason
        }
    }

    override mapToUpdate(param: AppointmentModel): AppointmentEntity {
        return {
            id: param.id,
            serviceId: param.service?.id,
            date: param.date,
            time: param.time,
            professionalId: param.professional?.id,
        }
    }

    override mapFromGet(param: AppointmentEntity): AppointmentModel {
        return {
            id: param.id,
            pet: this._petMapper.mapFromGet(param.pet!),
            service: this._serviceMapper.mapFromGet(param.service!),
            date: param.date,
            time: param.time,
            professional: this._professionalMapper.mapFromGet(param.professional!),
            reason: param.reason,
            status: param.status
        }
    }

    
    mapFromGetHour(param: AppointmentHourEntity): AppointmentHourModel {
        return {
            time: param.time
        }
    }

    mapToSearchFilterEntity(model: AppointmentSearchFilterModel): AppointmentSearchFilterEntity {
        return {
            date: model.date,
            query: model.query
        };
    }

}