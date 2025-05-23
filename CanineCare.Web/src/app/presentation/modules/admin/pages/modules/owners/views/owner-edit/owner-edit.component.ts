import { ActivatedRoute } from '@angular/router';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { ActionResult } from '@domain/base/action-result';
import { OwnerModel } from '@domain/models/people/owner-model';
import { OwnerUpdateUsecase } from '@domain/usecases/owner/owner-update-usecase';
import { OwnerGetByIdentificationUsecase } from '@domain/usecases/owner/owner-getbyidentification-usecase';

import { ToastService } from '@infrastructure/common/toast.service';
import { DialogService } from '@presentation/shared/services/dialog.service';
import { emailValidator } from '@presentation/shared/validators/email.validator';
import { phoneValidator } from '@presentation/shared/validators/phone.validator';
import { OnlyNamesDirective } from '@presentation/shared/directives/only-names.directive';
import { PhoneNumberDirective } from '@presentation/shared/directives/phone-number.directive';

@Component({
  selector: 'app-owner-edit',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    OnlyNamesDirective,
    PhoneNumberDirective
  ],
  templateUrl: './owner-edit.component.html',
  styleUrl: './owner-edit.component.css'
})
export class OwnerEditComponent implements OnInit, OnDestroy {

  public editForm!: FormGroup;
  public searchForm!: FormGroup;

  public isSearchResult = signal<boolean>(false);
  public isSubmittingEdit = signal<boolean>(false);

  private destroy$ = new Subject<void>();
  private updateResult$!: Observable<ActionResult>;
  private getByIdentificationResult$!: Observable<ActionResult<OwnerModel>>;

  constructor(
    private _fb: FormBuilder,
    private _toast: ToastService,
    private _route: ActivatedRoute,
    private _dialogService: DialogService,
    private _updateUseCase: OwnerUpdateUsecase,
    private _getByIdentificationUseCase: OwnerGetByIdentificationUsecase
  ) { }

  ngOnInit(): void {
    this.initializeForm();
    this.loadFromQueryParams();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onSearch(): void {
    if (this.isSubmittingEdit() || this.isSearchResult()) return;

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

  onSubmit(): void {
    if (this.isSubmittingEdit() || this.isSearchResult()) return;

    if (this.editForm.invalid) {
      this.editForm.markAllAsTouched();
      this._toast.showError('Por favor, verifica la información ingresada.', 'Atención');
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

  private handle(response: ActionResult): void {
    switch (response.statusCode) {
      case 200:
        this._toast.showSuccess(response.message);
        this.editForm.reset();
        this.searchForm.reset();
        break;
      default:
        this._toast.showError('Ocurrió un error inesperado. Por favor, inténtalo nuevamente.');
        this.editForm.reset();
        break;
    }
  }

  private loadFromQueryParams(): void {
    this._route.queryParams.subscribe(params => {
      const id = params['identification'];
      if (id) {
        this.searchForm.patchValue({ identification: id });
        this.onSearch();
      }
    });
  }

  private getData(): OwnerModel {
    return {
      id: this.id.value,
      firstName: this.firstName.value,
      lastName: this.lastName.value,
      email: this.email.value,
      phone: this.phone.value,
      address: this.address.value,
    };
  }

  private setData(model: OwnerModel): void {
    this.editForm.patchValue({
      id: model.id,
      firstName: model.firstName,
      lastName: model.lastName,
      email: model.email,
      phone: model.phone,
      address: model.address
    })
  }

  private initializeForm(): void {
    this.searchForm = this._fb.group({
      identification: ['', [Validators.required, Validators.maxLength(20)]],
    })

    this.editForm = this._fb.group({
      id: [''],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.maxLength(100), emailValidator()]],
      phone: ['', [Validators.required, Validators.maxLength(10), phoneValidator()]],
      address: ['', [Validators.required, Validators.maxLength(255)]],
    });
  }

  get id() { return this.editForm.get('id')! }
  get firstName() { return this.editForm.get('firstName')! }
  get lastName() { return this.editForm.get('lastName')! }
  get email() { return this.editForm.get('email')! }
  get phone() { return this.editForm.get('phone')! }
  get address() { return this.editForm.get('address')! }

  get identification() { return this.searchForm.get('identification')! }

}