import { Injectable } from "@angular/core";

import { GenericRepository } from "@domain/base/generic-repository";
import { ProfessionalModel } from "@domain/models/people/professional-model";

@Injectable({
    providedIn: 'root'
})
export abstract class ProfessionalRepository extends GenericRepository<ProfessionalModel> { }
