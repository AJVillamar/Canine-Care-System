import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { ActionResult } from "./action-result";

@Injectable({
    providedIn: 'root'
})
export abstract class SimpleRepository<T> {
    
    abstract create(entity: T): Observable<ActionResult>;
    
    abstract getAll(): Observable<ActionResult<T[]>>;

}
