import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { AppointmentModel } from "@domain/models/appointments/appointment-model";
import { AppointmentRepository } from "@domain/repositories/appointment/appointment-repository";

@Injectable({
    providedIn: 'root'
})
export class AppointmentCreateUsecase implements UseCase<AppointmentModel, ActionResult> {

    constructor(private _repository: AppointmentRepository) { }

    execute(params: AppointmentModel): Observable<ActionResult> {
        return this._repository.create(params);
    }

}
