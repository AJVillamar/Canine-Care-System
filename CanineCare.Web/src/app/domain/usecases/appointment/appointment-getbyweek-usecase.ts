import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { UseCase } from '@domain/base/use-case';
import { ActionResult } from '@domain/base/action-result';
import { AppointmentModel } from '@domain/models/appointments/appointment-model';
import { AppointmentRepository } from '@domain/repositories/appointment/appointment-repository';

@Injectable({
    providedIn: 'root'
})
export class AppointmentGetByWeekUsecase implements UseCase<Date, ActionResult<AppointmentModel[]>> {

    constructor(private readonly _repository: AppointmentRepository) { }

    execute(date: Date): Observable<ActionResult<AppointmentModel[]>> {
        return this._repository.getByWeek(date);
    }
    
}
