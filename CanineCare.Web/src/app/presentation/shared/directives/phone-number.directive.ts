import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
    selector: 'input[PhoneNumberOnly]',
    standalone: true,
})
export class PhoneNumberDirective {

    constructor(private readonly elRef: ElementRef) { }

    @HostListener('input', ['$event'])
    onInputChange(event: Event): void {
        const input = this.elRef.nativeElement as HTMLInputElement;

        let initialValue = input.value.replace(/[^0-9]/g, '');

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

        if (!/[0-9]/.test(event.key)) {
            event.preventDefault();
        }

        if (input.value.length >= 10) {
            event.preventDefault();
        }
    }
}
