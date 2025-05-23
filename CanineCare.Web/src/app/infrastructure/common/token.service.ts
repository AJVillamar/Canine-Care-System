import { jwtDecode } from 'jwt-decode';
import { Injectable } from '@angular/core';
import { LoggedUserModel } from '@domain/models/authentication/logged-user-model';

@Injectable({
    providedIn: 'root'
})
export class TokenService {

    private readonly storageKey = 'Canine_Care_Token';

    constructor() { }

    save(token: string): void {
        sessionStorage.setItem(this.storageKey, token);
    }

    remove(): void {
        sessionStorage.removeItem(this.storageKey);
    }

    get(): string | null {
        return sessionStorage.getItem(this.storageKey);
    }

    getLoggedUser(): LoggedUserModel | null {
        const token = this.get();
        if (!token) return null;

        try {
            const decoded: any = jwtDecode(token);
            const roles = Array.isArray(decoded.role) ? decoded.role : [decoded.role];
            return new LoggedUserModel(decoded.nameid, decoded.unique_name, roles);
        } catch (error) {
            console.error('Error decoding the token:', error);
            return null;
        }
    }

    isTokenExpired(): boolean {
        const expirationDate = this.getTokenExpiration();
        return expirationDate ? expirationDate < new Date() : true;
    }

    private getTokenExpiration(): Date | null {
        const token = this.get();
        if (!token) return null;
        try {
            const decodedToken = jwtDecode<{ exp: number }>(token);
            return decodedToken.exp ? new Date(decodedToken.exp * 1000) : null;
        } catch (error) {
            console.error('Error decoding token:', error);
            return null;
        }
    }
    
}
