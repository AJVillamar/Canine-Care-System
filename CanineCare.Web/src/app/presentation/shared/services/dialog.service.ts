import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ModelConfirmationComponent } from '../components/model-confirmation/model-confirmation.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private _dialog: MatDialog) { }

  confirm(isDangerous: boolean = false): Observable<boolean> {
    return this._dialog.open(ModelConfirmationComponent, {
      autoFocus: false,
      disableClose: true,
      width: 'auto',
      data: isDangerous
    }).afterClosed();
  }

}
