import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { ServiceModel } from "@domain/models/appointments/service-model";
import { ServiceRepository } from "@domain/repositories/appointment/service-repository";

@Injectable({
    providedIn: 'root'
})
export class ServiceCreateUsecase implements UseCase<ServiceModel, ActionResult> {

    constructor(private _repository: ServiceRepository) { }

    execute(params: ServiceModel): Observable<ActionResult> {
        return this._repository.create(params);
    }

}
