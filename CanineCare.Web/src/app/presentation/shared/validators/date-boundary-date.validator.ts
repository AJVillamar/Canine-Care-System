import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function dateBoundaryValidator(allowFutureOnly: boolean): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const value = control.value;

        if (!value) return null;

        const inputDate = new Date(value + 'T00:00:00');
        const today = new Date();

        today.setHours(0, 0, 0, 0);
        inputDate.setHours(0, 0, 0, 0);

        if (allowFutureOnly && inputDate.getTime() < today.getTime()) {
            return { onlyFuture: true };
        }

        if (!allowFutureOnly && inputDate.getTime() > today.getTime()) {
            return { onlyPast: true };
        }

        return null;
    };
}
