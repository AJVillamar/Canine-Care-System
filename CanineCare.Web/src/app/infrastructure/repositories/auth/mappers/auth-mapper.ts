import { CredentialEntity } from "../entities/auth-entity";

import { CredentialsModel } from "@domain/models/authentication/credentials-model";

export class AuthMapper {

    mapToLogin(param: CredentialsModel): CredentialEntity {
        return {
            identification: param.identification,
            password: param.password
        }
    }

}
