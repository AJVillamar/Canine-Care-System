import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function legalAgeValidator(): ValidatorFn {

    return (control: AbstractControl): ValidationErrors | null => {
        const value = control.value;

        if (!value) return null;

        const birthDate = new Date(value);
        const today = new Date();

        const age = today.getFullYear() - birthDate.getFullYear();
        const monthDifference = today.getMonth() - birthDate.getMonth();
        const dayDifference = today.getDate() - birthDate.getDate();

        const adjustedAge = monthDifference > 0 || (monthDifference === 0 && dayDifference >= 0) ? age : age - 1;

        if (adjustedAge < 18) {
            return { notAnAdult: true };
        }

        return null;
    };
}
