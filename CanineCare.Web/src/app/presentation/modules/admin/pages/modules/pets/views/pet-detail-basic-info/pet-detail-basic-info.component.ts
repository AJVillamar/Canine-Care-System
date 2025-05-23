import { Observable, Subject, takeUntil } from 'rxjs';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Component, OnInit, OnDestroy, signal, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';

import { PetModel } from '@domain/models/pet/pet-model';
import { ActionResult } from '@domain/base/action-result';
import { PetExtraInfoModel } from '@domain/models/pet/pet-extrainfo-model';
import { PetGetByIdUsecase } from '@domain/usecases/pet/pet-getbyid-usecase';

import { OwnerDetailComponent } from '../../../owners/views/owner-detail/owner-detail.component';
import { PetDetailAdditionalInfoComponent } from '../pet-detail-additional-info/pet-detail-additional-info.component';

@Component({
  selector: 'app-pet-detail-basic-info',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './pet-detail-basic-info.component.html',
  styleUrl: './pet-detail-basic-info.component.css'
})
export class PetDetailBasicInfoComponent implements OnInit, OnDestroy {

  public form!: FormGroup;
  public isLoadingData = signal<boolean>(false);
  private extraInfo: PetExtraInfoModel | null = null;

  private destroy$ = new Subject<void>();
  private getByIdResult$!: Observable<ActionResult<PetModel>>;

  constructor(
    private _fb: FormBuilder,
    private _dialog: MatDialog,
    private _getByIdUseCase: PetGetByIdUsecase,
    private _dialogRef: MatDialogRef<PetDetailBasicInfoComponent>,
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

  openDetailPetExtraInfo() {
    if (this.extraInfo == null) return

    this._dialog.open(PetDetailAdditionalInfoComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: this.extraInfo
    })
  }

  openDetailOwner() {
    if (this.ownerId.invalid) return
    const id = this.ownerId.value;

    this._dialog.open(OwnerDetailComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: id
    })
  }

  private initializeForm(): void {
    this.form = this._fb.group({
      name: [''],
      breed: [''],
      birthDate: [''],
      sex: [''],
      color: ['#000000'],
      weight: [''],
      ownerId: ['']
    });
  }

  private loadData(id: string): void {
    this.isLoadingData.set(true);
    this.getByIdResult$ = this._getByIdUseCase.execute(id);
    this.getByIdResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<PetModel>) => this.setData(response.data as PetModel),
      error: () => this.isLoadingData.set(false),
      complete: () => this.isLoadingData.set(false)
    });
  }

  private setData(data: PetModel): void {
    const formatDate = new Date(data.birthDate!);
    const isValidHexColor = /^#[0-9A-Fa-f]{6}$/.test(data.color || '');

    this.form.patchValue({
      name: data.name,
      breed: data.breed?.name,
      birthDate: formatDate.toISOString().split('T')[0],
      sex: data.sex,
      color: isValidHexColor ? data.color : '#000000',
      weight: data.weight,
      ownerId: data.owner?.id
    });

    if (data.extraInfo) this.extraInfo = data.extraInfo
  }

  get name() { return this.form.get('name')! }
  get breed() { return this.form.get('breed')! }
  get birthDate() { return this.form.get('birthDate')! }
  get sex() { return this.form.get('sex')! }
  get color() { return this.form.get('color')! }
  get weight() { return this.form.get('weight')! }
  get ownerId() { return this.form.get('ownerId')! }

}
