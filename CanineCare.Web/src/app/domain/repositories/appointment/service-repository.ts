import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { ActionResult } from "@domain/base/action-result";

import { GenericRepository } from "@domain/base/generic-repository";
import { ServiceModel } from "@domain/models/appointments/service-model";

@Injectable({
    providedIn: 'root'
})
export abstract class ServiceRepository extends GenericRepository<ServiceModel> { 

    abstract getAllType(): Observable<ActionResult<string[]>>;
    
}
