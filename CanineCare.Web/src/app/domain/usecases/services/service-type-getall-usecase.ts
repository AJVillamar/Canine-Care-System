import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { ServiceRepository } from '@domain/repositories/appointment/service-repository';

@Injectable({
    providedIn: 'root'
})
export class ServiceTypeGetAllUsecase implements UseCase<void, ActionResult<string[]>> {

    constructor(private _repository: ServiceRepository) { }

    execute(): Observable<ActionResult<string[]>> {
        return this._repository.getAllType();
    }

}
