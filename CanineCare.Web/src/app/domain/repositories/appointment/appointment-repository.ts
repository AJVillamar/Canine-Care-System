import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { ActionResult } from "@domain/base/action-result";

import { GenericRepository } from "@domain/base/generic-repository";
import { AppointmentModel } from "@domain/models/appointments/appointment-model";
import { AppointmentHourModel } from "@domain/models/appointments/appointment-hour-model";
import { AppointmentSearchFilterModel } from "@domain/models/appointments/appointment-searchfilter-model";

@Injectable({
    providedIn: 'root'
})
export abstract class AppointmentRepository extends GenericRepository<AppointmentModel> {

    abstract cancel(id: string): Observable<ActionResult>;

    abstract getHours(): Observable<ActionResult<AppointmentHourModel[]>>;

    abstract search(filter: AppointmentSearchFilterModel): Observable<ActionResult<AppointmentModel[]>>;

    abstract getByWeek(date: Date): Observable<ActionResult<AppointmentModel[]>>;

}
