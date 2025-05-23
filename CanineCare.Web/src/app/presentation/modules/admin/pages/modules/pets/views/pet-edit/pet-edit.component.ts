import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subject, takeUntil } from 'rxjs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { PetModel } from '@domain/models/pet/pet-model';
import { ActionResult } from '@domain/base/action-result';
import { PetBreedModel } from '@domain/models/pet/pet-breed-model';
import { PetUpdateUsecase } from '@domain/usecases/pet/pet-update-usecase';
import { PetBreedGetAllUsecase } from '@domain/usecases/pet/pet-breed-getall-usecase ';
import { PetGetByOwnerIdentificationUsecase } from '@domain/usecases/pet/pet-getby-owneridentification-usecase';

import { ToastService } from '@infrastructure/common/toast.service';
import { DialogService } from '@presentation/shared/services/dialog.service';
import { PriceDirective } from '@presentation/shared/directives/price.directive';
import { dateBoundaryValidator } from '@presentation/shared/validators/date-boundary-date.validator';

@Component({
  selector: 'app-pet-edit',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PriceDirective,
    MatTooltipModule
  ],
  templateUrl: './pet-edit.component.html',
  styleUrls: [
    './pet-edit.component.css',
    '../../../../../styles/step-indicator.styles.css'
  ]
})
export class PetEditComponent implements OnInit, OnDestroy {

  public currentStep: number = 1;
  public pets: PetModel[] = [];
  public breeds: PetBreedModel[] = [];
  public ownerFullName = "del cliente";

  public searchForm!: FormGroup;
  public basicInfoForm!: FormGroup;
  public additionalInfoForm!: FormGroup;

  public isSearchResult = signal<boolean>(false);
  public isSubmittingEdit = signal<boolean>(false);
  public isLoadingDropdowns = signal<boolean>(false);

  private destroy$ = new Subject<void>;
  private updateResult$!: Observable<ActionResult>;
  private getAllBreed$!: Observable<ActionResult<PetBreedModel[]>>;
  private getPetsByOwnerIdentificationResult$!: Observable<ActionResult<PetModel[]>>;

  public steps = [
    { id: 1, title: 'Información Básica', iconClass: 'fa-solid fa-paw' },
    { id: 2, title: 'Información Adicional', iconClass: 'fa-solid fa-shield-dog' },
  ];

  constructor(
    private _fb: FormBuilder,
    private _toast: ToastService,
    private _route: ActivatedRoute,
    private _dialogService: DialogService,
    private _updateUseCase: PetUpdateUsecase,
    private _getAllBreedUseCase: PetBreedGetAllUsecase,
    private _getByOwnerIdentificationUseCase: PetGetByOwnerIdentificationUsecase
  ) { }

  ngOnInit(): void {
    this.initializeForm();
    this.loadFromQueryParams();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  changeStep(step: number) {
    this.currentStep = step;
  }

  nextStep() {
    if (this.currentStep < this.steps.length) {
      this.currentStep++;
    }
  }

  previousStep() {
    if (this.currentStep > 1) {
      this.currentStep--;
    }
  }

  onSubmit(): void {
    if (
      this.isSubmittingEdit() ||
      this.isSearchResult() ||
      this.isLoadingDropdowns()
    ) return;

    if (this.basicInfoForm.invalid) {
      this.basicInfoForm.markAllAsTouched();
      this._toast.showError('Por favor, revisa y corrige la información básica.');
      this.currentStep = 1;
      return;
    }

    if (this.additionalInfoForm.invalid) {
      this.additionalInfoForm.markAllAsTouched();
      this._toast.showError('Por favor, revisa y corrige la información adicional.');
      this.currentStep = 2;
      return;
    }

    const body = this.getData();

    this._dialogService.confirm(true).subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isSubmittingEdit.set(true);
        this.updateResult$ = this._updateUseCase.execute(body);
        this.updateResult$.pipe(takeUntil(this.destroy$)).subscribe({
          next: (response: ActionResult) => this.handle(response),
          error: () => this.isSubmittingEdit.set(false),
          complete: () => this.isSubmittingEdit.set(false),
        });
      }
    });
  }

  onSearch(): void {
    if (
      this.isSubmittingEdit() ||
      this.isSearchResult() ||
      this.isLoadingDropdowns()
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

  onSelectPet(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    const petId = selectElement.value;

    const selectedPet = this.pets.find(p => p.id === petId);
    if (selectedPet) {
      this.setDataPet(selectedPet)
    }
  }

  hasPetData(): boolean {
    return !!this.pet.value?.trim();
  }

  resetSearch(): void {
    this.resetFrom();
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

  private setDataPet(data: PetModel): void {
    const formatDate = new Date(data.birthDate!);
    const isValidHexColor = /^#[0-9A-Fa-f]{6}$/.test(data.color || '');

    this.basicInfoForm.patchValue({
      petId: data.id,
      name: data.name,
      breed: data.breed?.id,
      birthDate: formatDate.toISOString().split('T')[0],
      sex: data.sex,
      color: isValidHexColor ? data.color : '#000000',
      weight: data.weight,
    })

    this.additionalInfoForm.patchValue({
      allergies: data.extraInfo?.allergies,
      preExistingConditions: data.extraInfo?.preExistingConditions,
      specialCareInstructions: data.extraInfo?.specialCareInstructions,
      feedingNotes: data.extraInfo?.feedingNotes,
    })
  }

  private handle(response: ActionResult): void {
    switch (response.statusCode) {
      case 200:
        this._toast.showSuccess(response.message);
        this.resetFrom();
        break;
      default:
        this._toast.showError('Ocurrió un error inesperado. Por favor, inténtalo nuevamente.');
        break;
    }
  }

  private loadFromQueryParams(): void {
    this._route.queryParams.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.searchForm.patchValue({ identification: id });
        this.onSearch();
      }
    });
  }

  private initializeForm(): void {
    this.searchForm = this._fb.group({
      identification: ['', [Validators.required, Validators.maxLength(20)]],
      pet: ['']
    })

    this.basicInfoForm = this._fb.group({
      petId: [''],
      name: ['', [Validators.required, Validators.maxLength(255)]],
      breed: ['', [Validators.required]],
      birthDate: ['', [Validators.required, dateBoundaryValidator(false)]],
      sex: ['', [Validators.required]],
      color: ['#000000', [Validators.required, Validators.maxLength(100)]],
      weight: ['', [Validators.required]],
    });

    this.additionalInfoForm = this._fb.group({
      allergies: ['', [Validators.required, Validators.maxLength(1000)]],
      preExistingConditions: ['', [Validators.required, Validators.maxLength(1000)]],
      specialCareInstructions: ['', [Validators.required, Validators.maxLength(1000)]],
      feedingNotes: ['', [Validators.required, Validators.maxLength(1000)]],
    });

    this.loadOptionsSelect();
  }

  private loadOptionsSelect() {
    this.isLoadingDropdowns.set(true);
    this.getAllBreed$ = this._getAllBreedUseCase.execute();
    this.getAllBreed$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<PetBreedModel[]>) => this.breeds = response.data as PetBreedModel[],
      error: () => this.isLoadingDropdowns.set(false),
      complete: () => this.isLoadingDropdowns.set(false)
    });
  }

  private getData(): PetModel {
    return {
      id: this.petId.value,
      name: this.name.value,
      breed: { id: this.breed.value },
      birthDate: this.birthDate.value,
      sex: this.sex.value,
      color: this.color.value,
      weight: this.weight.value,
      extraInfo: {
        allergies: this.allergies.value,
        preExistingConditions: this.preExistingConditions.value,
        specialCareInstructions: this.specialCareInstructions.value,
        feedingNotes: this.feedingNotes.value
      }
    }
  }

  private resetFrom(): void {
    this.searchForm.reset();
    this.pet.setValue('');
    this.pets = [];
    this.basicInfoForm.reset();
    this.breed.setValue('');
    this.sex.setValue('');
    this.color.setValue('#000000');
    this.additionalInfoForm.reset();
    this.currentStep = 1;
  }

  get pet() { return this.searchForm.get('pet')! }
  get identification() { return this.searchForm.get('identification')! }

  get petId() { return this.basicInfoForm.get('petId')! }
  get name() { return this.basicInfoForm.get('name')! }
  get breed() { return this.basicInfoForm.get('breed')! }
  get birthDate() { return this.basicInfoForm.get('birthDate')! }
  get sex() { return this.basicInfoForm.get('sex')! }
  get color() { return this.basicInfoForm.get('color')! }
  get weight() { return this.basicInfoForm.get('weight')! }

  get allergies() { return this.additionalInfoForm.get('allergies')! }
  get preExistingConditions() { return this.additionalInfoForm.get('preExistingConditions')! }
  get specialCareInstructions() { return this.additionalInfoForm.get('specialCareInstructions')! }
  get feedingNotes() { return this.additionalInfoForm.get('feedingNotes')! }

}