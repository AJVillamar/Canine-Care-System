import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { AppointmentHourModel } from "@domain/models/appointments/appointment-hour-model";
import { AppointmentRepository } from "@domain/repositories/appointment/appointment-repository";

@Injectable({
    providedIn: 'root'
})
export class AppointmentGetHoursUsecase implements UseCase<void, ActionResult<AppointmentHourModel[]>> {

    constructor(private _repository: AppointmentRepository) { }

    execute(): Observable<ActionResult<AppointmentHourModel[]>> {
        return this._repository.getHours();
    }

}
