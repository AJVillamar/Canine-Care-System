import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function phoneValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {

        const value = control.value;

        if (!value) return null;

        if (!value.startsWith('09')) {
            return { invalidPhonePrefix: true };
        }

        if (value.length !== 10) {
            return { invalidPhoneLength: true };
        }

        return null;
    };
}