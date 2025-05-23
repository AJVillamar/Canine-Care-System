import { CommonModule } from '@angular/common';
import { Observable, Subject, takeUntil } from 'rxjs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Component, Inject, OnDestroy, OnInit, signal } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';

import { ToastService } from '@infrastructure/common/toast.service';
import { ServiceDetailComponent } from '../../../services/views/service-detail/service-detail.component';

import { ActionResult } from '@domain/base/action-result';
import { AppointmentModel } from '@domain/models/appointments/appointment-model';
import { AppointmentGetByIdUsecase } from '@domain/usecases/appointment/appointment-getbyid-usecase';
import { PetDetailBasicInfoComponent } from '../../../pets/views/pet-detail-basic-info/pet-detail-basic-info.component';
import { ProfessionalDetailComponent } from '../../../professionals/views/professional-detail/professional-detail.component';


@Component({
  selector: 'app-appointment-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatTooltipModule,
    ReactiveFormsModule
  ],
  templateUrl: './appointment-detail.component.html',
  styleUrl: './appointment-detail.component.css'
})
export class AppointmentDetailComponent implements OnInit, OnDestroy {

  public form!: FormGroup;
  public isLoading = signal<boolean>(false);
  public appointmet: AppointmentModel | null = null;

  private destroy$ = new Subject<void>;
  private getByIdResult$!: Observable<ActionResult<AppointmentModel>>;

  constructor(
    private _fb: FormBuilder,
    private _dialog: MatDialog,
    private _toast: ToastService,
    private _getByIdAppoitment: AppointmentGetByIdUsecase,
    private _dialogRef: MatDialogRef<AppointmentDetailComponent>,
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

  openServiceDetail(): void {
    if (!this.appointmet) {
      this._toast.showInfo('Lo sentimos la información aún no esta disponible.');
      return;
    }

    this._dialog.open(ServiceDetailComponent, {
      autoFocus: false,
      disableClose: true,
      width: '550px',
      data: this.appointmet?.service?.id
    });
  }

  openPetDetail() {
    if (!this.appointmet) {
      this._toast.showInfo('Lo sentimos la información aún no esta disponible.');
      return;
    }

    this._dialog.open(PetDetailBasicInfoComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: this.appointmet?.pet?.id
    })
  }

  openProfessionalDetail() {
    if (!this.appointmet) {
      this._toast.showInfo('Lo sentimos la información aún no esta disponible.');
      return;
    }

    this._dialog.open(ProfessionalDetailComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: this.appointmet.professional?.id
    })
  }

  private initializeForm(): void {
    this.form = this._fb.group({
      pet: [''],
      serviceType: [''],
      serviceSelect: [''],
      date: [''],
      hour: [''],
      professional: [''],
      reason: [''],
      status: ['']
    })
  }

  private setData(data: AppointmentModel): void {
    this.appointmet = data;

    this.form.patchValue({
      pet: data.pet?.name,
      serviceType: data.service?.type,
      serviceSelect: data.service?.name,
      date: data.date,
      hour: data.time?.slice(0, 5),
      professional: `${data.professional?.firstName} ${data.professional?.lastName}`,
      reason: data.reason,
      status: data.status
    });
  }

  private loadData(id: string): void {
    this.isLoading.set(true);
    this.getByIdResult$ = this._getByIdAppoitment.execute(id);
    this.getByIdResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<AppointmentModel>) => this.setData(response.data as AppointmentModel),
      error: () => this.isLoading.set(false),
      complete: () => this.isLoading.set(false)
    });
  }

}
