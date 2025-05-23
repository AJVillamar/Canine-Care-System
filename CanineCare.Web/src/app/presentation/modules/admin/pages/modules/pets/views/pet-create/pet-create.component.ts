import { CommonModule } from '@angular/common';
import { Observable, Subject, takeUntil } from 'rxjs';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { PetModel } from '@domain/models/pet/pet-model';
import { ActionResult } from '@domain/base/action-result';
import { OwnerModel } from '@domain/models/people/owner-model';
import { PetBreedModel } from '@domain/models/pet/pet-breed-model';
import { PetCreateUsecase } from '@domain/usecases/pet/pet-create-usecase';
import { PetBreedGetAllUsecase } from '@domain/usecases/pet/pet-breed-getall-usecase ';
import { OwnerGetByIdentificationUsecase } from '@domain/usecases/owner/owner-getbyidentification-usecase';

import { ToastService } from '@infrastructure/common/toast.service';
import { DialogService } from '@presentation/shared/services/dialog.service';
import { PriceDirective } from '@presentation/shared/directives/price.directive';
import { dateBoundaryValidator } from '@presentation/shared/validators/date-boundary-date.validator';
 
@Component({
  selector: 'app-pet-create',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PriceDirective,
    MatTooltipModule
  ],
  templateUrl: './pet-create.component.html',
  styleUrls: [
    './pet-create.component.css',
    '../../../../../styles/step-indicator.styles.css'
  ] 
})
export class PetCreateComponent implements OnInit, OnDestroy {

  public currentStep: number = 1;
  public breeds: PetBreedModel[] = [];

  public searchForm!: FormGroup;
  public basicInfoForm!: FormGroup;
  public additionalInfoForm!: FormGroup;

  public isSubmitting = signal<boolean>(false);
  public isSearchResult = signal<boolean>(false);
  public isLoadingDropdowns = signal<boolean>(false);

  private destroy$ = new Subject<void>;
  private createResult$!: Observable<ActionResult>;
  private getAllBreed$!: Observable<ActionResult<PetBreedModel[]>>;
  private getByIdentificationResult$!: Observable<ActionResult<OwnerModel>>;

  public steps = [
    { id: 1, title: 'Información Básica', iconClass: 'fa-solid fa-paw' },
    { id: 2, title: 'Información Adicional', iconClass: 'fa-solid fa-shield-dog' },
  ];

  constructor(
    private _fb: FormBuilder,
    private _toast: ToastService,
    private _dialogService: DialogService,
    private _createUseCase: PetCreateUsecase,
    private _getAllBreedUseCase: PetBreedGetAllUsecase,
    private _getByIdentificationUseCase: OwnerGetByIdentificationUsecase
  ) { }

  ngOnInit(): void {
    this.initializeForm();
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
      this.isSubmitting() ||
      this.isSearchResult() ||
      this.isLoadingDropdowns()
    ) return;

    if (this.searchForm.invalid) {
      this.basicInfoForm.markAllAsTouched();
      this._toast.showError('Por favor, ingrese un número de cédula para realizar la búsqueda.');
      this.currentStep = 1;
      return;
    }

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
        this.isSubmitting.set(true);
        this.createResult$ = this._createUseCase.execute(body);
        this.createResult$.pipe(takeUntil(this.destroy$)).subscribe({
          next: (response: ActionResult) => this.handle(response),
          error: () => this.isSubmitting.set(false),
          complete: () => this.isSubmitting.set(false),
        });
      }
    })
  }

  onSearch(): void {
    if (
      this.isSubmitting() ||
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
    this.getByIdentificationResult$ = this._getByIdentificationUseCase.execute(param);
    this.getByIdentificationResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<OwnerModel>) => this.setData(response.data as OwnerModel),
      error: () => {
        this.searchForm.reset()
        this.isSearchResult.set(false)
      },
      complete: () => this.isSearchResult.set(false),
    });
  }

  hasOwnerData(): boolean {
    return !!this.ownerFullName.value?.trim();
  }

  resetSearch(): void {
    this.searchForm.reset();
    this.basicInfoForm.patchValue({ ownerId: '' });
  }

  private setData(data: OwnerModel): void {
    const fullName = `${data.firstName} ${data.lastName}`.trim();
    this.searchForm.patchValue({ ownerFullName: fullName });
    this.basicInfoForm.patchValue({ ownerId: data.id })
  }

  private handle(response: ActionResult): void {
    switch (response.statusCode) {
      case 201:
        this._toast.showSuccess(response.message);
        this.searchForm.reset();
        this.basicInfoForm.reset();
        this.additionalInfoForm.reset();
        this.currentStep = 1;
        break;
      default:
        this._toast.showError('Ocurrió un error inesperado. Por favor, inténtalo nuevamente.');
        break;
    }
  }

  private initializeForm(): void {
    this.searchForm = this._fb.group({
      identification: ['', [Validators.required, Validators.maxLength(20)]],
      ownerFullName: ['']
    })

    this.basicInfoForm = this._fb.group({
      ownerId: ['', [Validators.required]],
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
    this.getAllBreed$ = this._getAllBreedUseCase.execute();
    this.getAllBreed$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<PetBreedModel[]>) => this.breeds = response.data as PetBreedModel[],
      error: () => this.isLoadingDropdowns.set(false),
      complete: () => this.isLoadingDropdowns.set(false)
    });
  }

  private getData(): PetModel {
    return {
      name: this.name.value,
      breed: { id: this.breed.value },
      birthDate: this.birthDate.value,
      sex: this.sex.value,
      color: this.color.value,
      weight: this.weight.value,
      owner: { id: this.ownerId.value },
      extraInfo: {
        allergies: this.allergies.value,
        preExistingConditions: this.preExistingConditions.value,
        specialCareInstructions: this.specialCareInstructions.value,
        feedingNotes: this.feedingNotes.value
      }
    }
  }

  get identification() { return this.searchForm.get('identification')! }
  get ownerFullName() { return this.searchForm.get('ownerFullName')! }

  get name() { return this.basicInfoForm.get('name')! }
  get breed() { return this.basicInfoForm.get('breed')! }
  get birthDate() { return this.basicInfoForm.get('birthDate')! }
  get sex() { return this.basicInfoForm.get('sex')! }
  get color() { return this.basicInfoForm.get('color')! }
  get weight() { return this.basicInfoForm.get('weight')! }
  get ownerId() { return this.basicInfoForm.get('ownerId')! }

  get allergies() { return this.additionalInfoForm.get('allergies')! }
  get preExistingConditions() { return this.additionalInfoForm.get('preExistingConditions')! }
  get specialCareInstructions() { return this.additionalInfoForm.get('specialCareInstructions')! }
  get feedingNotes() { return this.additionalInfoForm.get('feedingNotes')! }

} 
