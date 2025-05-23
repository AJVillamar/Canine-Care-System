import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { ServiceModel } from "@domain/models/appointments/service-model";
import { ServiceRepository } from "@domain/repositories/appointment/service-repository";

@Injectable({
    providedIn: 'root'
})
export class ServiceGetAllUsecase implements UseCase<void, ActionResult<ServiceModel[]>> {

    constructor(private _repository: ServiceRepository) { }

    execute(): Observable<ActionResult<ServiceModel[]>> {
        return this._repository.getAll();
    }

}
