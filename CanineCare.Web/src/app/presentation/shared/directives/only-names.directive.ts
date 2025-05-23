import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
    selector: 'input[OnlyNames]',
    standalone: true,
})
export class OnlyNamesDirective {

    constructor(private readonly elRef: ElementRef) { }

    @HostListener('input', ['$event'])
    onInputChange(event: Event): void {

        const input = this.elRef.nativeElement as HTMLInputElement;
        const validPattern = /^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]*$/;
        const maxWords = 2;

        let initialValue = input.value;

        initialValue = initialValue.split('')
            .filter(char => validPattern.test(char))
            .join('');

        const words = initialValue.trim().split(/\s+/);
        if (words.length > maxWords) {
            initialValue = words.slice(0, maxWords).join(' ');
        }

        if (input.value !== initialValue) {
            input.value = initialValue;
            event.stopPropagation();
        }
    }

    @HostListener('keypress', ['$event'])
    onKeyPress(event: KeyboardEvent): void {

        const input = this.elRef.nativeElement as HTMLInputElement;
        const allowedKeys = /^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]*$/;

        if (!allowedKeys.test(event.key)) {
            event.preventDefault();
        }

        const words = input.value.trim().split(/\s+/);
        if (words.length >= 2 && event.key === ' ') {
            event.preventDefault();
        }
    }

}
