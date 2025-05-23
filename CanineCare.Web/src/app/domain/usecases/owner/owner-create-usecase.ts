import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { OwnerModel } from "@domain/models/people/owner-model";
import { OwnerRepository } from "@domain/repositories/people/owner-repository";

@Injectable({
    providedIn: 'root'
})
export class OwnerCreateUsecase implements UseCase<OwnerModel, ActionResult> {

    constructor(private _repository: OwnerRepository) { }

    execute(params: OwnerModel): Observable<ActionResult> {
        return this._repository.create(params);
    }

}
