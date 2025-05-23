import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ActionResult } from '@domain/base/action-result';
import { CredentialsModel } from '@domain/models/authentication/credentials-model';
import { AuthRepository } from '@domain/repositories/authentication/auth-repository';

import { AuthMapper } from '../mappers/auth-mapper';
import { CredentialEntity } from '../entities/auth-entity';
import { environment } from 'src/environments/environment';

import { TokenService } from '@infrastructure/common/token.service';
import { ApiResponse } from '@infrastructure/shared/entities/api-response';
import { ResponseMapper } from '@infrastructure/shared/mappers/response-mapper';

@Injectable({
  providedIn: 'root'
})
export class AuthRepositoryImplService extends AuthRepository {

  private readonly _apiUrl = `${environment.endpoint}Authentication/`;
  private readonly _authMapper = new AuthMapper();
  private readonly _responseMapper = new ResponseMapper();

  constructor(
    private _token: TokenService,
    private readonly _http: HttpClient,
  ) { super() }

  override login(credentials: CredentialsModel): Observable<ActionResult<string>> {
    const body: CredentialEntity = this._authMapper.mapToLogin(credentials);
    return this._http
      .post<ApiResponse<string>>(`${this._apiUrl}login`, body)
      .pipe(
        map((response) => {
          const token = response.data as string;
          if (response.statusCode === 200) {
            this._token.save(token);
          }
          return this._responseMapper.mapFromActionResult(response);
        })
      );
  }

}
