import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
    selector: 'input[PriceOnly]',
    standalone: true,
})
export class PriceDirective {
    constructor(private readonly elRef: ElementRef) { }

    @HostListener('keydown', ['$event'])
    onKeyDown(event: KeyboardEvent): void {
        if (['e', 'E', '+', '-'].includes(event.key)) {
            event.preventDefault();
        }
    }

    @HostListener('input', ['$event'])
    onInputChange(event: Event): void {
        const input = this.elRef.nativeElement as HTMLInputElement;

        let value = input.value.replace(/[^0-9.,]/g, '');

        value = value.replace(/,/g, '.');

        const parts = value.split('.');
        if (parts.length > 2) {
            value = parts[0] + '.' + parts[1];
        }

        if (parts[1]?.length > 2) {
            value = parts[0] + '.' + parts[1].substring(0, 2);
        }

        if (value.startsWith('0') && value.length > 1 && !value.includes('.')) {
            value = value.substring(1);
        }

        if (input.value !== value) {
            input.value = value;
            event.stopPropagation();
        }
    }
}
