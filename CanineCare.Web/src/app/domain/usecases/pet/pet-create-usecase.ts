import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { PetModel } from "@domain/models/pet/pet-model";
import { PetRepository } from "@domain/repositories/pet/pet-repository";

@Injectable({
    providedIn: 'root'
})
export class PetCreateUsecase implements UseCase<PetModel, ActionResult> {

    constructor(private _repository: PetRepository) { }

    execute(params: PetModel): Observable<ActionResult> {
        return this._repository.create(params);
    }

}
