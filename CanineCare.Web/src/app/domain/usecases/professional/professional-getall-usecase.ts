import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { UseCase } from "@domain/base/use-case";
import { ActionResult } from "@domain/base/action-result";
import { ProfessionalModel } from "@domain/models/people/professional-model";
import { ProfessionalRepository } from "@domain/repositories/people/professional-repository";

@Injectable({
    providedIn: 'root'
})
export class ProfessionalGetAllUsecase implements UseCase<void, ActionResult<ProfessionalModel[]>> {

    constructor(private _repository: ProfessionalRepository) { }

    execute(): Observable<ActionResult<ProfessionalModel[]>> {
        return this._repository.getAll();
    }

}
