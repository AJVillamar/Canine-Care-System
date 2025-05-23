import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { ActionResult } from "@domain/base/action-result";

import { PetModel } from "@domain/models/pet/pet-model";
import { GenericRepository } from "@domain/base/generic-repository";
import { PetBreedModel } from "@domain/models/pet/pet-breed-model";

@Injectable({
    providedIn: 'root'
})
export abstract class PetRepository extends GenericRepository<PetModel> {

    abstract getAllBreed(): Observable<ActionResult<PetBreedModel[]>>;
    
    abstract getAllByOwnerIdentification(identification: string): Observable<ActionResult<PetModel[]>>;

}
