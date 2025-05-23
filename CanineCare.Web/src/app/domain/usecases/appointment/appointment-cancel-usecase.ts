import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { UseCase } from '@domain/base/use-case';
import { ActionResult } from '@domain/base/action-result';
import { AppointmentRepository } from '@domain/repositories/appointment/appointment-repository';

@Injectable({
    providedIn: 'root'
})
export class AppointmentCancelUsecase implements UseCase<string, ActionResult> {

    constructor(private readonly _repository: AppointmentRepository) { }

    execute(appointmentId: string): Observable<ActionResult> {
        return this._repository.cancel(appointmentId);
    }
    
}
