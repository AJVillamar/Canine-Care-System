import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { UseCase } from '@domain/base/use-case';
import { ActionResult } from '@domain/base/action-result';
import { AppointmentModel } from '@domain/models/appointments/appointment-model';
import { AppointmentRepository } from '@domain/repositories/appointment/appointment-repository';

@Injectable({
    providedIn: 'root'
})
export class AppointmentUpdateUsecase implements UseCase<AppointmentModel, ActionResult> {

    constructor(private readonly _repository: AppointmentRepository) { }

    execute(appointment: AppointmentModel): Observable<ActionResult> {
        return this._repository.update(appointment);
    }

}
