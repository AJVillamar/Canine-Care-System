import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ActionResult } from '@domain/base/action-result';
import { AppointmentModel } from '@domain/models/appointments/appointment-model';
import { AppointmentHourModel } from '@domain/models/appointments/appointment-hour-model';
import { AppointmentRepository } from '@domain/repositories/appointment/appointment-repository';
import { AppointmentSearchFilterModel } from '@domain/models/appointments/appointment-searchfilter-model';

import { ApiResponse } from '@infrastructure/shared/entities/api-response';
import { ResponseMapper } from '@infrastructure/shared/mappers/response-mapper';

import { environment } from 'src/environments/environment';
import { AppointmentMapper } from '../mappers/appointment-mapper';
import { AppointmentEntity } from '../entities/appointment-entity';
import { AppointmentSearchFilterEntity } from '../entities/appointment-searchfilter-entity';

@Injectable({
    providedIn: 'root'
})
export class AppointmentRepositoryImplService extends AppointmentRepository {

    private readonly _apiAppointmentUrl = `${environment.endpoint}Appointment/`;
    private readonly _appointmentMapper = new AppointmentMapper();
    private readonly _responseMapper = new ResponseMapper();

    constructor(private readonly _http: HttpClient) { super() }

    override create(entity: AppointmentModel): Observable<ActionResult> {
        const body: AppointmentEntity = this._appointmentMapper.mapToCreate(entity);
        return this._http
            .post<ApiResponse>(this._apiAppointmentUrl, body)
            .pipe(map(response => this._responseMapper.mapFromActionResult(response)));
    }

    override update(entity: AppointmentModel): Observable<ActionResult> {
        const body: AppointmentEntity = this._appointmentMapper.mapToUpdate(entity);
        return this._http
            .put<ApiResponse>(this._apiAppointmentUrl, body)
            .pipe(map(response => this._responseMapper.mapFromActionResult(response)));
    }

    override cancel(id: string): Observable<ActionResult> {
        return this._http
            .delete<ApiResponse>(`${this._apiAppointmentUrl}${id}`)
            .pipe(map(response => this._responseMapper.mapFromActionResult(response)));
    }

    override getById(id: string): Observable<ActionResult<AppointmentModel>> {
        return this._http
            .get<ApiResponse<AppointmentEntity>>(`${this._apiAppointmentUrl}by-id/${id}`)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? this._appointmentMapper.mapFromGet(response.data) : undefined
            })));
    }

    override getAll(): Observable<ActionResult<AppointmentModel[]>> {
        return this._http
            .get<ApiResponse<AppointmentEntity[]>>(this._apiAppointmentUrl)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? response.data.map(entity => this._appointmentMapper.mapFromGet(entity)) : undefined
            })));
    }

    override getHours(): Observable<ActionResult<AppointmentHourModel[]>> {
        return this._http
            .get<ApiResponse<AppointmentHourModel[]>>(`${this._apiAppointmentUrl}hours`)
            .pipe(
                map(response => ({
                    ...this._responseMapper.mapFromActionResult(response),
                    data: response.data ? response.data.map(entity => this._appointmentMapper.mapFromGetHour(entity)) : undefined
                }))
            );
    }

    override search(filter: AppointmentSearchFilterModel): Observable<ActionResult<AppointmentModel[]>> {
        const body: AppointmentSearchFilterEntity = this._appointmentMapper.mapToSearchFilterEntity(filter);

        return this._http
            .post<ApiResponse<AppointmentEntity[]>>(`${this._apiAppointmentUrl}search`, body)
            .pipe(
                map((response) => ({
                    ...this._responseMapper.mapFromActionResult(response),
                    data: response.data ? response.data.map(entity => this._appointmentMapper.mapFromGet(entity)) : undefined
                }))
            );
    }

    override getByWeek(date: Date): Observable<ActionResult<AppointmentModel[]>> {
        const formattedDate = date.toISOString().split('T')[0];

        return this._http
            .get<ApiResponse<AppointmentEntity[]>>(`${this._apiAppointmentUrl}week`, { params: { date: formattedDate } })
            .pipe(
                map((response) => ({
                    ...this._responseMapper.mapFromActionResult(response),
                    data: response.data ? response.data.map(entity => this._appointmentMapper.mapFromGet(entity)) : undefined
                }))
            );
    }

}
