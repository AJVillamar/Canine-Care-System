import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { PetModel } from "@domain/models/pet/pet-model";
import { ActionResult } from "@domain/base/action-result";
import { PetRepository } from "@domain/repositories/pet/pet-repository";

@Injectable({
    providedIn: 'root'
})
export class PetGetByOwnerIdentificationUsecase implements UseCase<string, ActionResult<PetModel[]>> {

    constructor(private _repository: PetRepository) { }

    execute(params: string): Observable<ActionResult<PetModel[]>> {
        return this._repository.getAllByOwnerIdentification(params);
    }

}
