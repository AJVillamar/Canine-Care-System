import { Observable, Subject, takeUntil } from 'rxjs';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Component, OnInit, OnDestroy, signal, Inject } from '@angular/core';

import { ActionResult } from '@domain/base/action-result';
import { ProfessionalModel } from '@domain/models/people/professional-model';
import { ProfessionalGetByIdUsecase } from '@domain/usecases/professional/professional-getbyid-usecase';

@Component({
  selector: 'app-professional-detail',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './professional-detail.component.html',
  styleUrl: './professional-detail.component.css'
})
export class ProfessionalDetailComponent implements OnInit, OnDestroy {

  public form!: FormGroup;
  public isLoadingData = signal<boolean>(false);

  private destroy$ = new Subject<void>();
  private getByIdResult$!: Observable<ActionResult<ProfessionalModel>>;

  constructor(
    private _fb: FormBuilder,
    private _getByIdUseCase: ProfessionalGetByIdUsecase,
    private _dialogRef: MatDialogRef<ProfessionalDetailComponent>,
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
    this._dialogRef.close(false)
  }

  private initializeForm(): void {
    this.form = this._fb.group({
      identification: [''],
      lastName: [''],
      firstName: [''],
      email: [''],
      birthDate: [''],
      yearsOfExperience: [''],
    });
  }

  private loadData(id: string): void {
    this.isLoadingData.set(true);
    this.getByIdResult$ = this._getByIdUseCase.execute(id);
    this.getByIdResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<ProfessionalModel>) => this.setData(response.data as ProfessionalModel),
      error: () => this.isLoadingData.set(false),
      complete: () => this.isLoadingData.set(false)
    });
  }

  private setData(data: ProfessionalModel): void {
    const formatDate = new Date(data.birthDate!);

    this.form.patchValue({
      identification: data.identification,
      lastName: data.lastName,
      firstName: data.firstName,
      email: data.email,
      birthDate: formatDate.toISOString().split('T')[0],
      yearsOfExperience: data.yearsOfExperience,
    });
  }

  get identification() { return this.form.get('identification')! }
  get firstName() { return this.form.get('firstName')! }
  get lastName() { return this.form.get('lastName')! }
  get email() { return this.form.get('email')! }
  get birthDate() { return this.form.get('birthDate')! }
  get yearsOfExperience() { return this.form.get('yearsOfExperience')! }

}
