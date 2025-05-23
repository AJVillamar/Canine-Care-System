import { Observable, Subject, takeUntil } from 'rxjs';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { ActionResult } from '@domain/base/action-result';
import { ProfessionalModel } from '@domain/models/people/professional-model';
import { ProfessionalCreateUsecase } from '@domain/usecases/professional/professional-create-usecase';

import { ToastService } from '@infrastructure/common/toast.service';
import { DialogService } from '@presentation/shared/services/dialog.service';
import { emailValidator } from '@presentation/shared/validators/email.validator';
import { legalAgeValidator } from '@presentation/shared/validators/legal-age.validator';
import { OnlyNamesDirective } from '@presentation/shared/directives/only-names.directive';
import { YearsExperienceDirective } from '@presentation/shared/directives/years-experience.directive';

@Component({
  selector: 'app-professional-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    OnlyNamesDirective,
    YearsExperienceDirective
  ],
  templateUrl: './professional-create.component.html',
  styleUrl: './professional-create.component.css'
})
export class ProfessionalCreateComponent implements OnInit, OnDestroy {

  public form!: FormGroup;
  public isSubmitting = signal<boolean>(false);

  private destroy$ = new Subject<void>;
  private createResult$!: Observable<ActionResult>;

  constructor(
    private _fb: FormBuilder,
    private _toast: ToastService,
    private _dialogService: DialogService,
    private _createUseCase: ProfessionalCreateUsecase,
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
    })
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

  private getData(): ProfessionalModel {
    return {
      identification: this.identification.value,
      firstName: this.firstName.value,
      lastName: this.lastName.value,
      email: this.email.value,
      birthDate: this.birthDate.value,
      yearsOfExperience: this.yearsOfExperience.value
    }
  }

  private initializeForm(): void {
    this.form = this._fb.group({
      identification: ['', [Validators.required, Validators.maxLength(20)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.maxLength(100), emailValidator()]],
      birthDate: ['', [Validators.required, legalAgeValidator()]],
      yearsOfExperience: ['', [Validators.required]],
    });
  }

  get identification() { return this.form.get('identification')! }
  get firstName() { return this.form.get('firstName')! }
  get lastName() { return this.form.get('lastName')! }
  get email() { return this.form.get('email')! }
  get birthDate() { return this.form.get('birthDate')! }
  get yearsOfExperience() { return this.form.get('yearsOfExperience')! }

}
