import { CommonModule } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { forkJoin, Observable, Subject, takeUntil } from 'rxjs';
import { Component, Inject, OnDestroy, OnInit, signal } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { ActionResult } from '@domain/base/action-result';
import { ServiceModel } from '@domain/models/appointments/service-model';
import { ProfessionalModel } from '@domain/models/people/professional-model';
import { DialogService } from '@presentation/shared/services/dialog.service';
import { AppointmentModel } from '@domain/models/appointments/appointment-model';
import { ServiceGetAllUsecase } from '@domain/usecases/services/service-getall-usecase';
import { AppointmentHourModel } from '@domain/models/appointments/appointment-hour-model';
import { ServiceTypeGetAllUsecase } from '@domain/usecases/services/service-type-getall-usecase';
import { AppointmentUpdateUsecase } from '@domain/usecases/appointment/appointment-update-usecase';
import { AppointmentGetByIdUsecase } from '@domain/usecases/appointment/appointment-getbyid-usecase';
import { ProfessionalGetAllUsecase } from '@domain/usecases/professional/professional-getall-usecase';
import { AppointmentGetHoursUsecase } from '@domain/usecases/appointment/appointment-gethours-usecase';

import { ToastService } from '@infrastructure/common/toast.service';
import { dateBoundaryValidator } from '@presentation/shared/validators/date-boundary-date.validator';
import { ServiceDetailComponent } from '../../../services/views/service-detail/service-detail.component';
import { ProfessionalDetailComponent } from '../../../professionals/views/professional-detail/professional-detail.component';

@Component({
  selector: 'app-appointment-update',
  standalone: true,
  imports: [
    CommonModule,
    MatTooltipModule,
    ReactiveFormsModule
  ],
  templateUrl: './appointment-update.component.html',
  styleUrl: './appointment-update.component.css'
})
export class AppointmentUpdateComponent implements OnInit, OnDestroy {

  public servicesType: string[] = [];
  public servicesAll: ServiceModel[] = [];
  public servicesSelectTypes: ServiceModel[] = [];
  public hours: AppointmentHourModel[] = [];
  public professionals: ProfessionalModel[] = [];

  public form!: FormGroup;

  public isLoadingData = signal<boolean>(false);
  public isSubmitting = signal<boolean>(false);
  public isLoadingDropdowns = signal<boolean>(false);

  private destroy$ = new Subject<void>;
  private updateResult$!: Observable<ActionResult>;
  private getByIdResult$!: Observable<ActionResult<AppointmentModel>>;

  constructor(
    private _fb: FormBuilder,
    private _dialog: MatDialog,
    private _toast: ToastService,
    private _dialogService: DialogService,
    private _updateUseCase: AppointmentUpdateUsecase,
    private _getAllServicesUseCase: ServiceGetAllUsecase,
    private _getAllTypesUseCase: ServiceTypeGetAllUsecase,
    private _getAllHoursUseCase: AppointmentGetHoursUsecase,
    private _getAllProfessionalUseCase: ProfessionalGetAllUsecase,
    private _getByIdAppoitment: AppointmentGetByIdUsecase,
    private _dialogRef: MatDialogRef<AppointmentUpdateComponent>,
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

  onSelectServiceType(event: Event): void {
    const selectedType = (event.target as HTMLSelectElement).value;

    this.servicesSelectTypes = this.servicesAll.filter(
      service => service.type?.toLowerCase() === selectedType.toLowerCase()
    );

    this.serviceSelect.setValue('');
  }

  openServiceDetail(id: string): void {
    if (!id) {
      this._toast.showInfo('Selecciona primero un servicio.');
      return;
    }

    this._dialog.open(ServiceDetailComponent, {
      autoFocus: false,
      disableClose: true,
      width: '550px',
      data: id
    });
  }

  openProfessionalDetail(id: string) {
    if (!id) {
      this._toast.showInfo('Selecciona primero una mascota.');
      return;
    }

    this._dialog.open(ProfessionalDetailComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: id
    })
  }

  onSubmit(): void {
    if (this.isSubmitting()) return;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this._toast.showError('Por favor, verifica la información ingresada.', 'Atención');
      return;
    }

    const body = this.getData();

    this._dialogService.confirm(true).subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isSubmitting.set(true);
        this.updateResult$ = this._updateUseCase.execute(body);
        this.updateResult$.pipe(takeUntil(this.destroy$)).subscribe({
          next: (response: ActionResult) => this.handle(response),
          error: () => this.isSubmitting.set(false),
          complete: () => this.isSubmitting.set(false),
        });
      }
    });
  }

  private loadData(id: string): void {
    this.isLoadingData.set(true);
    this.getByIdResult$ = this._getByIdAppoitment.execute(id);
    this.getByIdResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<AppointmentModel>) => this.setData(response.data as AppointmentModel),
      error: () => this.isLoadingData.set(false),
      complete: () => this.isLoadingData.set(false)
    });
  }

  private handle(response: ActionResult): void {
    switch (response.statusCode) {
      case 200:
        this._dialogRef.close(response);
        break;
      default:
        this._toast.showError('Ocurrió un error inesperado. Por favor, inténtalo nuevamente.');
        break;
    }
  }

  private getData(): AppointmentModel {
    return {
      id: this.id,
      service: { id: this.serviceSelect.value },
      date: this.date.value,
      time: this.hour.value,
      professional: { id: this.professional.value },
    }
  }

  private setData(data: AppointmentModel): void {
    this.servicesSelectTypes = this.servicesAll.filter(
      service => service.type?.toLowerCase() === data.service?.type?.toLowerCase());

    this.form.patchValue({
      serviceType: data.service?.type,
      serviceSelect: data.service?.id,
      date: data.date,
      hour: data.time,
      professional: data.professional?.id,
    });
  }

  private initializeForm(): void {
    this.form = this._fb.group({
      serviceType: ['', [Validators.required]],
      serviceSelect: ['', [Validators.required]],
      date: ['', [Validators.required, dateBoundaryValidator(true)]],
      hour: ['', [Validators.required]],
      professional: ['', [Validators.required]],
    })

    this.loadOptionsSelect();
  }

  private loadOptionsSelect() {
    this.isLoadingDropdowns.set(true);
    forkJoin([
      this._getAllTypesUseCase.execute(),
      this._getAllServicesUseCase.execute(),
      this._getAllHoursUseCase.execute(),
      this._getAllProfessionalUseCase.execute(),
    ]).pipe(takeUntil(this.destroy$)).subscribe({
      next: ([typesResponse, servicesResponse, hoursResponse, professionalsResponse]) => {
        this.servicesType = typesResponse.data as string[];
        this.servicesAll = servicesResponse.data as ServiceModel[];
        this.hours = hoursResponse.data as AppointmentHourModel[];
        this.professionals = professionalsResponse.data as ProfessionalModel[];
      },
      error: () => this.isLoadingDropdowns.set(false),
      complete: () => this.isLoadingDropdowns.set(false)
    });
  }

  get serviceType() { return this.form.get('serviceType')! }
  get serviceSelect() { return this.form.get('serviceSelect')! }
  get date() { return this.form.get('date')! }
  get hour() { return this.form.get('hour')! }
  get professional() { return this.form.get('professional')! }

}
