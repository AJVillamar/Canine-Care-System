import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})
export class ToastService {

    constructor(private toast: ToastrService) { }

    showSuccess(message: string, title: string = 'Éxito'): void {
        this.toast.success(message, title);
    }

    showError(message: string, title: string = 'Error'): void {
        this.toast.error(message, title);
    }

    showWarning(message: string, title: string = 'Advertencia'): void {
        this.toast.warning(message, title);
    }

    showInfo(message: string, title: string = 'Información'): void {
        this.toast.info(message, title);
    }
    
}
