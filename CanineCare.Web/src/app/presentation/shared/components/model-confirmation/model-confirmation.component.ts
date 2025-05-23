import { MatIcon } from '@angular/material/icon';
import { Component, Inject, signal } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-model-confirmation',
  standalone: true,
  imports: [MatIcon],
  templateUrl: './model-confirmation.component.html',
  styleUrl: './model-confirmation.component.css'
})
export class ModelConfirmationComponent {

  public isSuccessAction   = signal<boolean>(false)

  constructor(
    @Inject(MAT_DIALOG_DATA) public isSuccess: boolean,
    private _dialogRef: MatDialogRef<ModelConfirmationComponent>,
  ) { }

  ngOnInit(): void {
    this.isSuccessAction  .set(this.isSuccess)
  }

  onCancel(): void {
    this._dialogRef.close(false)
  }

  onConfirm(): void {
    this._dialogRef.close(true)
  }

}
