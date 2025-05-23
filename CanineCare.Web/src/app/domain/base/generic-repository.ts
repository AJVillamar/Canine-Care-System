import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { ActionResult } from "./action-result";

@Injectable({
    providedIn: 'root'
})
export abstract class GenericRepository<T> {

    abstract create(entity: T): Observable<ActionResult>;

    abstract update(entity: T): Observable<ActionResult>;

    abstract getById(id: string): Observable<ActionResult<T>>;

    abstract getAll(): Observable<ActionResult<T[]>>;
    
}
