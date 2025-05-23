import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { forkJoin, Observable, Subject, takeUntil } from 'rxjs';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { PetModel } from '@domain/models/pet/pet-model';
import { ActionResult } from '@domain/base/action-result';
import { ServiceModel } from '@domain/models/appointments/service-model';
import { ProfessionalModel } from '@domain/models/people/professional-model';
import { AppointmentModel } from '@domain/models/appointments/appointment-model';
import { ServiceGetAllUsecase } from '@domain/usecases/services/service-getall-usecase';
import { AppointmentHourModel } from '@domain/models/appointments/appointment-hour-model';
import { ServiceTypeGetAllUsecase } from '@domain/usecases/services/service-type-getall-usecase';
import { AppointmentCreateUsecase } from '@domain/usecases/appointment/appointment-create-usecase';
import { ProfessionalGetAllUsecase } from '@domain/usecases/professional/professional-getall-usecase';
import { AppointmentGetHoursUsecase } from '@domain/usecases/appointment/appointment-gethours-usecase';
import { PetGetByOwnerIdentificationUsecase } from '@domain/usecases/pet/pet-getby-owneridentification-usecase';

import { ToastService } from '@infrastructure/common/toast.service';
import { DialogService } from '@presentation/shared/services/dialog.service';
import { dateBoundaryValidator } from '@presentation/shared/validators/date-boundary-date.validator';
import { ServiceDetailComponent } from '../../../services/views/service-detail/service-detail.component';
import { PetDetailBasicInfoComponent } from '../../../pets/views/pet-detail-basic-info/pet-detail-basic-info.component';
import { ProfessionalDetailComponent } from '../../../professionals/views/professional-detail/professional-detail.component';

@Component({
  selector: 'app-appointment-create',
  standalone: true,
  imports: [
    CommonModule,
    MatTooltipModule,
    ReactiveFormsModule
  ],
  templateUrl: './appointment-create.component.html',
  styleUrl: './appointment-create.component.css'
})
export class AppointmentCreateComponent implements OnInit, OnDestroy {

  public pets: PetModel[] = [];
  public selectedPet: PetModel | null = null;

  public servicesType: string[] = [];
  public servicesAll: ServiceModel[] = [];
  public servicesSelectTypes: ServiceModel[] = [];
  public hours: AppointmentHourModel[] = [];
  public professionals: ProfessionalModel[] = [];

  public searchForm!: FormGroup;
  public createForm!: FormGroup;

  public isSubmitting = signal<boolean>(false);
  public isSearchResult = signal<boolean>(false);
  public isLoadingDropdowns = signal<boolean>(false);

  private destroy$ = new Subject<void>;
  private createResult$!: Observable<ActionResult>;
  private getPetsByOwnerIdentificationResult$!: Observable<ActionResult<PetModel[]>>;

  constructor(
    private _fb: FormBuilder,
    private _dialog: MatDialog,
    private _toast: ToastService,
    private _dialogService: DialogService,
    private _createUseCase: AppointmentCreateUsecase,
    private _getAllServicesUseCase: ServiceGetAllUsecase,
    private _getAllTypesUseCase: ServiceTypeGetAllUsecase,
    private _getAllHoursUseCase: AppointmentGetHoursUsecase,
    private _getAllProfessionalUseCase: ProfessionalGetAllUsecase,
    private _getByOwnerIdentificationUseCase: PetGetByOwnerIdentificationUsecase,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onSearch(): void {
    if (
      this.isSearchResult()
    ) return;

    if (this.searchForm.invalid) {
      this.searchForm.markAllAsTouched();
      this._toast.showError('Por favor, ingrese un número de cédula válida.');
      return;
    }

    const param = this.identification.value;

    this.isSearchResult.set(true);
    this.getPetsByOwnerIdentificationResult$ = this._getByOwnerIdentificationUseCase.execute(param);
    this.getPetsByOwnerIdentificationResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<PetModel[]>) => this.setDataPets(response),
      error: () => {
        this.identification.reset()
        this.isSearchResult.set(false)
      },
      complete: () => this.isSearchResult.set(false),
    });
  }

  private setDataPets(response: ActionResult<PetModel[]>): void {
    const data = response.data as PetModel[];
    if (data.length == 0) {
      this._toast.showInfo("El cliente no tiene mascotas registradas.");
      this.resetFrom();
      return;
    }

    this.pets = data;
    this._toast.showInfo("Mascotas obtenidas con éxito, seleccione una a editar.");
  }

  onSelectPet(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    const petId = selectElement.value;

    const selectedPet = this.pets.find(p => p.id === petId);
    if (selectedPet) {
      this.selectedPet = selectedPet
    }
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

  openPetDetail(id: string) {
    if (!id) {
      this._toast.showInfo('Selecciona primero una mascota.');
      return;
    }

    this._dialog.open(PetDetailBasicInfoComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: id
    })
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

  resetSearch(): void {
    this.resetFrom();
  }

  onSubmit(): void {
    if (this.isSubmitting()) return;

    if (this.pet.invalid) {
      this.pet.markAllAsTouched();
      this._toast.showError('Por favor, seleccione un canino.', 'Atención');
      return
    }


    if (this.createForm.invalid) {
      this.createForm.markAllAsTouched();
      this._toast.showError('Por favor, verifica la información ingresada.', 'Atención');
      return;
    }

    const body = this.getData();

    this._dialogService.confirm(true).subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isSubmitting.set(true);
        this.createResult$ = this._createUseCase.execute(body);
        this.createResult$.pipe(takeUntil(this.destroy$)).subscribe({
          next: (response: ActionResult) => this.handle(response),
          error: () => this.isSubmitting.set(false),
          complete: () => this.isSubmitting.set(false),
        });
      }
    });
  }

  private getData(): AppointmentModel {
    return {
      pet: { id: this.pet.value },
      service: { id: this.serviceSelect.value },
      date: this.date.value,
      time: this.hour.value,
      professional: { id: this.professional.value },
      reason: this.reason.value
    }
  }

  private handle(response: ActionResult): void {
    switch (response.statusCode) {
      case 201:
        this._toast.showSuccess(response.message);
        this.resetFrom();
        break;
      default:
        this._toast.showError('Ocurrió un error inesperado. Por favor, inténtalo nuevamente.');
        break;
    }
  }

  private resetFrom(): void {
    this.searchForm.reset();
    this.pet.setValue('');
    this.pets = [];
    this.selectedPet = null;
    this.createForm.reset();
    this.serviceType.setValue('');
    this.serviceSelect.setValue('');
    this.hour.setValue('');
    this.professional.setValue('');
  }

  private initializeForm(): void {
    this.searchForm = this._fb.group({
      identification: ['', [Validators.required, Validators.maxLength(20)]],
      pet: ['']
    })

    this.createForm = this._fb.group({
      serviceType: ['', [Validators.required]],
      serviceSelect: ['', [Validators.required]],
      date: ['', [Validators.required, dateBoundaryValidator(true)]],
      hour: ['', [Validators.required]],
      professional: ['', [Validators.required]],
      reason: ['']
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

  get identification() { return this.searchForm.get('identification')! }
  get pet() { return this.searchForm.get('pet')! }

  get serviceType() { return this.createForm.get('serviceType')! }
  get serviceSelect() { return this.createForm.get('serviceSelect')! }
  get date() { return this.createForm.get('date')! }
  get hour() { return this.createForm.get('hour')! }
  get professional() { return this.createForm.get('professional')! }
  get reason() { return this.createForm.get('reason')! }

}
