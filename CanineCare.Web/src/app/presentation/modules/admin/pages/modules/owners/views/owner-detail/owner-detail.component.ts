import { Observable, Subject, takeUntil } from 'rxjs';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Component, OnInit, OnDestroy, signal, Inject } from '@angular/core';

import { ActionResult } from '@domain/base/action-result';
import { OwnerModel } from '@domain/models/people/owner-model';
import { OwnerGetByIdUsecase } from '@domain/usecases/owner/owner-getbyid-usecase';

@Component({
  selector: 'app-owner-detail',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './owner-detail.component.html',
  styleUrl: './owner-detail.component.css'
})
export class OwnerDetailComponent implements OnInit, OnDestroy {

  public form!: FormGroup;
  public isLoadingData = signal<boolean>(false);

  private destroy$ = new Subject<void>();
  private getByIdResult$!: Observable<ActionResult<OwnerModel>>;

  constructor(
    private _fb: FormBuilder,
    private _getByIdUseCase: OwnerGetByIdUsecase,
    private _dialogRef: MatDialogRef<OwnerDetailComponent>,
    @Inject(MAT_DIALOG_DATA) public id: string
  ) { }

  ngOnInit(): void {
    this.initializeForm();
    this.loadData(this.id);
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  } 

  onCancel(): void {
    this._dialogRef.close(false);
  }

  private initializeForm(): void {
    this.form = this._fb.group({
      identification: [''],
      lastName: [''],
      firstName: [''],
      email: [''],
      phone: [''],
      address: [''],
    });
  }

  private loadData(id: string): void {
    this.isLoadingData.set(true);
    this.getByIdResult$ = this._getByIdUseCase.execute(id);
    this.getByIdResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<OwnerModel>) => this.setData(response.data as OwnerModel),
      error: () => this.isLoadingData.set(false),
      complete: () => this.isLoadingData.set(false)
    });
  }

  private setData(data: OwnerModel): void {
    this.form.patchValue({
      identification: data.identification,
      lastName: data.lastName,
      firstName: data.firstName,
      email: data.email,
      phone: data.phone,
      address: data.address,
    });
  }

  get identification() { return this.form.get('identification')! }
  get firstName() { return this.form.get('firstName')! }
  get lastName() { return this.form.get('lastName')! }
  get email() { return this.form.get('email')! }
  get phone() { return this.form.get('phone')! }
  get address() { return this.form.get('address')! }

}
