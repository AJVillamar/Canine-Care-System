import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { OwnerModel } from "@domain/models/people/owner-model";
import { OwnerRepository } from "@domain/repositories/people/owner-repository";

@Injectable({
    providedIn: 'root'
})
export class OwnerGetAllUsecase implements UseCase<void, ActionResult<OwnerModel[]>> {

    constructor(private _repository: OwnerRepository) { }

    execute(): Observable<ActionResult<OwnerModel[]>> {
        return this._repository.getAll();
    }

}
