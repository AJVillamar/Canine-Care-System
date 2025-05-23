import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { OwnerModel } from "@domain/models/people/owner-model";
import { OwnerRepository } from "@domain/repositories/people/owner-repository";

@Injectable({
    providedIn: 'root'
})
export class OwnerGetByIdentificationUsecase implements UseCase<string, ActionResult<OwnerModel>> {

    constructor(private _repository: OwnerRepository) { }

    execute(params: string): Observable<ActionResult<OwnerModel>> {
        return this._repository.getByIdentification(params);
    }

}
