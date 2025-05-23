import { Observable } from "rxjs";
import { Injectable } from "@angular/core";

import { ActionResult } from "@domain/base/action-result";
import { CredentialsModel } from "@domain/models/authentication/credentials-model";

@Injectable({
    providedIn: 'root'
})
export abstract class AuthRepository {

    abstract login(credentials: CredentialsModel): Observable<ActionResult<string>>;
    
}
