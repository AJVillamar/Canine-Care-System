import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ActionResult } from '@domain/base/action-result';
import { ServiceModel } from '@domain/models/appointments/service-model';
import { ServiceType } from '@domain/models/appointments/service-type-model';
import { ServiceRepository } from '@domain/repositories/appointment/service-repository';

import { ApiResponse } from '@infrastructure/shared/entities/api-response';
import { ResponseMapper } from '@infrastructure/shared/mappers/response-mapper';

import { ServiceMapper } from '../mappers/service-mapper';
import { environment } from 'src/environments/environment';
import { ServiceEntity } from '../entities/service-entity';

@Injectable({
    providedIn: 'root'
})
export class ServiceRepositoryImplService extends ServiceRepository {

    private readonly _apiUrl = `${environment.endpoint}Service/`;

    private readonly _serviceMapper = new ServiceMapper();
    private readonly _responseMapper = new ResponseMapper();

    constructor(private readonly _http: HttpClient) { super() }

    override create(entity: ServiceModel): Observable<ActionResult> {
        const body: ServiceEntity = this._serviceMapper.mapToCreate(entity);
        return this._http
            .post<ApiResponse>(this._apiUrl, body)
            .pipe(map(response => this._responseMapper.mapFromActionResult(response)));
    }

    override update(entity: ServiceModel): Observable<ActionResult> {
        throw new Error('Method not implemented.');
    }

    override getById(id: string): Observable<ActionResult<ServiceModel>> {
        return this._http
            .get<ApiResponse<ServiceEntity>>(`${this._apiUrl}by-id/${id}`)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? this._serviceMapper.mapFromGet(response.data) : undefined
            })));
    }


    override getAll(): Observable<ActionResult<ServiceModel[]>> {
        return this._http
            .get<ApiResponse<ServiceEntity[]>>(this._apiUrl)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? response.data.map(entity => this._serviceMapper.mapFromGet(entity)) : undefined
            })));
    }

    override getAllType(): Observable<ActionResult<string[]>> {
        const types = Object.values(ServiceType);
        return new Observable((observer) => {
            observer.next({
                statusCode: 200,
                message: 'Tipos de servicio obtenidos localmente',
                data: types
            });
            observer.complete();
        });
    }

}
