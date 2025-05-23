import { Observable, Subject, takeUntil } from 'rxjs';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { ActionResult } from '@domain/base/action-result';
import { OwnerModel } from '@domain/models/people/owner-model';
import { OwnerCreateUsecase } from '@domain/usecases/owner/owner-create-usecase';

import { ToastService } from '@infrastructure/common/toast.service';
import { DialogService } from '@presentation/shared/services/dialog.service';
import { emailValidator } from '@presentation/shared/validators/email.validator';
import { phoneValidator } from '@presentation/shared/validators/phone.validator';
import { OnlyNamesDirective } from '@presentation/shared/directives/only-names.directive';
import { PhoneNumberDirective } from '@presentation/shared/directives/phone-number.directive';

@Component({
  selector: 'app-owner-create',
  standalone: true, 
  imports: [
    ReactiveFormsModule,
    OnlyNamesDirective, 
    PhoneNumberDirective
  ],
  templateUrl: './owner-create.component.html',
  styleUrl: './owner-create.component.css'
})
export class OwnerCreateComponent implements OnInit, OnDestroy {

  public form!: FormGroup;
  public isSubmitting = signal<boolean>(false);

  private destroy$ = new Subject<void>();
  private createResult$!: Observable<ActionResult>;

  constructor(
    private _fb: FormBuilder,
    private _toast: ToastService,
    private _dialogService: DialogService,
    private _createUseCase: OwnerCreateUsecase,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
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
        this.createResult$ = this._createUseCase.execute(body);
        this.createResult$.pipe(takeUntil(this.destroy$)).subscribe({
          next: (response: ActionResult) => this.handle(response),
          error: () => this.isSubmitting.set(false),
          complete: () => this.isSubmitting.set(false),
        });
      }
    });
  }

  private handle(response: ActionResult): void {
    switch (response.statusCode) {
      case 201:
        this._toast.showSuccess(response.message);
        this.form.reset();
        break;
      default:
        this._toast.showError('Ocurrió un error inesperado. Por favor, inténtalo nuevamente.');
        this.form.reset();
        break;
    }
  }

  private getData(): OwnerModel {
    return {
      identification: this.identification.value,
      firstName: this.firstName.value,
      lastName: this.lastName.value,
      email: this.email.value,
      phone: this.phone.value,
      address: this.address.value,
    };
  }

  private initializeForm(): void {
    this.form = this._fb.group({
      identification: ['', [Validators.required, Validators.maxLength(20)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.maxLength(100), emailValidator()]],
      phone: ['', [Validators.required, Validators.maxLength(10), phoneValidator()]],
      address: ['', [Validators.required, Validators.maxLength(255)]],
    });
  }

  get identification() { return this.form.get('identification')! }
  get firstName() { return this.form.get('firstName')! }
  get lastName() { return this.form.get('lastName')! }
  get email() { return this.form.get('email')! }
  get phone() { return this.form.get('phone')! }
  get address() { return this.form.get('address')! }

}