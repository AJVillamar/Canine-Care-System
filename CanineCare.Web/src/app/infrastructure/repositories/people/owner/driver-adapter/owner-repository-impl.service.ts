import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ActionResult } from '@domain/base/action-result';
import { OwnerModel } from '@domain/models/people/owner-model';
import { OwnerRepository } from '@domain/repositories/people/owner-repository';

import { ApiResponse } from '@infrastructure/shared/entities/api-response';
import { ResponseMapper } from '@infrastructure/shared/mappers/response-mapper';

import { OwnerMapper } from '../mappers/owner-mapper';
import { OwnerEntity } from '../entities/owner-entity';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class OwnerRepositoryImplService extends OwnerRepository {

    private readonly _apiUrl = `${environment.endpoint}Owner/`;
    private readonly _ownerMapper = new OwnerMapper();
    private readonly _responseMapper = new ResponseMapper();

    constructor(private readonly _http: HttpClient) { super() }

    override create(entity: OwnerModel): Observable<ActionResult> {
        const body: OwnerEntity = this._ownerMapper.mapToCreate(entity);
        return this._http
            .post<ApiResponse>(this._apiUrl, body)
            .pipe(map(response => this._responseMapper.mapFromActionResult(response)));
    }

    override update(entity: OwnerModel): Observable<ActionResult> {
        const body: OwnerEntity = this._ownerMapper.mapToUpdate(entity);
        return this._http
            .put<ApiResponse>(this._apiUrl, body)
            .pipe(map(response => this._responseMapper.mapFromActionResult(response)));
    }

    override getById(id: string): Observable<ActionResult<OwnerModel>> {
        return this._http
            .get<ApiResponse<OwnerEntity>>(`${this._apiUrl}by-id/${id}`)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? this._ownerMapper.mapFromGet(response.data) : undefined
            })));
    }

    override getByIdentification(id: string): Observable<ActionResult<OwnerModel>> {
        return this._http
            .get<ApiResponse<OwnerEntity>>(`${this._apiUrl}by-identification/${id}`)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? this._ownerMapper.mapFromGet(response.data) : undefined
            })));
    }

    override getAll(): Observable<ActionResult<OwnerModel[]>> {
        return this._http
            .get<ApiResponse<OwnerEntity[]>>(this._apiUrl)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? response.data.map(entity => this._ownerMapper.mapFromGet(entity)) : undefined
            })));
    }

}
