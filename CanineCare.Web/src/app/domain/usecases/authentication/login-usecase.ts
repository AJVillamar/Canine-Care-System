import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { AuthRepository } from "@domain/repositories/authentication/auth-repository";
import { CredentialsModel } from "@domain/models/authentication/credentials-model";

@Injectable({
    providedIn: 'root'
})
export class LoginUsecase implements UseCase<CredentialsModel, ActionResult<string>> {

    constructor(private repository: AuthRepository) { }

    execute(params: CredentialsModel): Observable<ActionResult<string>> {
        return this.repository.login(params);
    }

}
