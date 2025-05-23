import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { ActionResult } from "@domain/base/action-result";

import { OwnerModel } from "@domain/models/people/owner-model";
import { GenericRepository } from "@domain/base/generic-repository";

@Injectable({
    providedIn: 'root'
})
export abstract class OwnerRepository extends GenericRepository<OwnerModel> { 

    abstract getByIdentification(identification: string): Observable<ActionResult<OwnerModel>>;
    
}
