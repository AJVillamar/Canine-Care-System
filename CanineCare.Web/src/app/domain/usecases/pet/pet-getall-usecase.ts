import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { PetModel } from "@domain/models/pet/pet-model";
import { ActionResult } from "@domain/base/action-result";
import { PetRepository } from "@domain/repositories/pet/pet-repository";

@Injectable({
    providedIn: 'root'
})
export class PetGetAllUsecase implements UseCase<void, ActionResult<PetModel[]>> {

    constructor(private _repository: PetRepository) { }

    execute(): Observable<ActionResult<PetModel[]>> {
        return this._repository.getAll();
    }

}
