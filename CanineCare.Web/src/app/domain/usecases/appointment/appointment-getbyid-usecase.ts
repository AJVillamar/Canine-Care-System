import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { AppointmentModel } from "@domain/models/appointments/appointment-model";
import { AppointmentRepository } from "@domain/repositories/appointment/appointment-repository";

@Injectable({
    providedIn: 'root'
})
export class AppointmentGetByIdUsecase implements UseCase<string, ActionResult<AppointmentModel>> {

    constructor(private _repository: AppointmentRepository) { }

    execute(params: string): Observable<ActionResult<AppointmentModel>> {
        return this._repository.getById(params);
    }

}
