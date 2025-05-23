import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ApiResponse } from '@infrastructure/shared/entities/api-response';
import { ResponseMapper } from '@infrastructure/shared/mappers/response-mapper';

import { environment } from 'src/environments/environment';
import { ProfessionalMapper } from '../mappers/professional-mapper';
import { ProfessionalEntity } from '../entities/professional-entity';

import { ActionResult } from '@domain/base/action-result';
import { ProfessionalModel } from '@domain/models/people/professional-model';
import { ProfessionalRepository } from '@domain/repositories/people/professional-repository';

@Injectable({
    providedIn: 'root'
})
export class ProfessionalRepositoryImplService extends ProfessionalRepository {

    private readonly _apiUrl = `${environment.endpoint}Professional/`;
    private readonly _professionalMapper = new ProfessionalMapper();
    private readonly _responseMapper = new ResponseMapper();

    constructor(private readonly _http: HttpClient) { super() }

    override create(entity: ProfessionalModel): Observable<ActionResult> {
        const body: ProfessionalModel = this._professionalMapper.mapToCreate(entity);
        return this._http
            .post<ApiResponse>(this._apiUrl, body)
            .pipe(map(response => this._responseMapper.mapFromActionResult(response)))
    }

    override update(entity: ProfessionalModel): Observable<ActionResult> {
        const body: ProfessionalEntity = this._professionalMapper.mapToUpdate(entity);
        return this._http
            .put<ApiResponse>(this._apiUrl, body)
            .pipe(map(response => this._responseMapper.mapFromActionResult(response)));
    }

    override getById(id: string): Observable<ActionResult<ProfessionalModel>> {
        return this._http
            .get<ApiResponse<ProfessionalEntity>>(`${this._apiUrl}by-id/${id}`)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? this._professionalMapper.mapFromGet(response.data) : undefined
            })));
    }

    override getAll(): Observable<ActionResult<ProfessionalModel[]>> {
        return this._http
            .get<ApiResponse<ProfessionalEntity[]>>(this._apiUrl)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? response.data.map(entity => this._professionalMapper.mapFromGet(entity)) : undefined
            })));
    }

}
