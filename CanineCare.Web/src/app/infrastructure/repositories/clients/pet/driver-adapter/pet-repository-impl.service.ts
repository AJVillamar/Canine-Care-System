import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ApiResponse } from '@infrastructure/shared/entities/api-response';
import { ResponseMapper } from '@infrastructure/shared/mappers/response-mapper';

import { PetMapper } from '../mappers/pet-mapper';
import { PetEntity } from '../entities/pet-entity';
import { environment } from 'src/environments/environment';

import { PetModel } from '@domain/models/pet/pet-model';
import { ActionResult } from '@domain/base/action-result';
import { PetBreedEntity } from '../entities/pet-breed-entity';
import { PetBreedModel } from '@domain/models/pet/pet-breed-model';
import { PetRepository } from '@domain/repositories/pet/pet-repository';

@Injectable({
    providedIn: 'root'
})
export class PetRepositoryImplService extends PetRepository {

    private readonly _apiPetUrl = `${environment.endpoint}Pet/`;
    private readonly _apiBreedUrl = `${environment.endpoint}Breed/`;
    
    private readonly _petMapper = new PetMapper();
    private readonly _responseMapper = new ResponseMapper();

    constructor(private readonly _http: HttpClient) { super() }

    override create(entity: PetModel): Observable<ActionResult> {
        const body: PetEntity = this._petMapper.mapToCreate(entity);
        return this._http
            .post<ApiResponse>(this._apiPetUrl, body)
            .pipe(map(response => this._responseMapper.mapFromActionResult(response)));
    }

    override update(entity: PetModel): Observable<ActionResult> {
        const body: PetEntity = this._petMapper.mapToUpdate(entity);
        return this._http
            .put<ApiResponse>(this._apiPetUrl, body)
            .pipe(map(response => this._responseMapper.mapFromActionResult(response)));
    }

    override getById(id: string): Observable<ActionResult<PetModel>> {
        return this._http
            .get<ApiResponse<PetEntity>>(`${this._apiPetUrl}by-id/${id}`)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? this._petMapper.mapFromGet(response.data) : undefined
            })));
    }

    override getAllByOwnerIdentification(identification: string): Observable<ActionResult<PetModel[]>> {
        return this._http
            .get<ApiResponse<PetEntity[]>>(`${this._apiPetUrl}by-identification/${identification}`)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? response.data.map(entity => this._petMapper.mapFromGet(entity)) : undefined
            })));
    }

    override getAll(): Observable<ActionResult<PetModel[]>> {
        return this._http
            .get<ApiResponse<PetEntity[]>>(this._apiPetUrl)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? response.data.map(entity => this._petMapper.mapFromGet(entity)) : undefined
            })));
    }

    override getAllBreed(): Observable<ActionResult<PetBreedModel[]>> {
        return this._http
            .get<ApiResponse<PetBreedEntity[]>>(this._apiBreedUrl)
            .pipe(map((response) => ({
                ...this._responseMapper.mapFromActionResult(response),
                data: response.data ? response.data.map(entity => this._petMapper.mapFromGetBreed(entity)) : undefined
            })));
    }

}
