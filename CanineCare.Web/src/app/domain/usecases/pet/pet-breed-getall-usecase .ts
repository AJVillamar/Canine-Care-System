import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { PetBreedModel } from "@domain/models/pet/pet-breed-model";
import { PetRepository } from "@domain/repositories/pet/pet-repository";

@Injectable({
    providedIn: 'root'
})
export class PetBreedGetAllUsecase implements UseCase<void, ActionResult<PetBreedModel[]>> {

    constructor(private _repository: PetRepository) { }

    execute(): Observable<ActionResult<PetBreedModel[]>> {
        return this._repository.getAllBreed();
    }

}
