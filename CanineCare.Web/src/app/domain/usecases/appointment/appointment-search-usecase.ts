import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { AppointmentModel } from "@domain/models/appointments/appointment-model";
import { AppointmentRepository } from "@domain/repositories/appointment/appointment-repository";
import { AppointmentSearchFilterModel } from "@domain/models/appointments/appointment-searchfilter-model";

@Injectable({
    providedIn: 'root'
})
export class AppointmentSearchUsecase implements UseCase<AppointmentSearchFilterModel, ActionResult<AppointmentModel[]>> {

    constructor(private _repository: AppointmentRepository) { }

    execute(params: AppointmentSearchFilterModel): Observable<ActionResult<AppointmentModel[]>> {
        return this._repository.search(params);
    }

}
