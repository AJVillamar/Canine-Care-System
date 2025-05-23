import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
    selector: 'input[YearsExperience]',
    standalone: true,
})
export class YearsExperienceDirective {
    constructor(private readonly elRef: ElementRef) { }

    @HostListener('input', ['$event'])
    onInputChange(event: Event): void {
        const input = this.elRef.nativeElement as HTMLInputElement;

        const numbersOnly = /[^0-9]*/g;

        let initialValue = input.value;

        initialValue = initialValue.replace(numbersOnly, '');

        if (initialValue.startsWith('0')) {
            initialValue = initialValue.slice(1);
        }

        if (initialValue.length > 10) {
            initialValue = initialValue.slice(0, 10);
        }

        if (input.value !== initialValue) {
            input.value = initialValue;
            event.stopPropagation();
        }
    }

    @HostListener('keypress', ['$event'])
    onKeyPress(event: KeyboardEvent): void {
        const input = this.elRef.nativeElement as HTMLInputElement;

        if (input.value.length >= 10) {
            event.preventDefault();
        }

        if (input.value.length === 0 && event.key === '0') {
            event.preventDefault();
        }
    }
}
