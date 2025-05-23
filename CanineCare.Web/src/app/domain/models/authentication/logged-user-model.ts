export class LoggedUserModel {
    id: string;
    fullName: string;
    role: string | string[];

    constructor(id: string, fullName: string, role: string[]) {
        this.id = id;
        this.fullName = fullName;
        this.role = role;
    }
}
