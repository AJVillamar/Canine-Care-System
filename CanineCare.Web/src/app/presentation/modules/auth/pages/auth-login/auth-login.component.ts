import { Router } from '@angular/router';
import { Observable, Subject, takeUntil } from 'rxjs';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { ActionResult } from '@domain/base/action-result';
import { LoginUsecase } from '@domain/usecases/authentication/login-usecase';
import { CredentialsModel } from '@domain/models/authentication/credentials-model';

import { TokenService } from '@infrastructure/common/token.service';
import { ToastService } from '@infrastructure/common/toast.service';
import { APP_ROUTES_MODULES } from '@presentation/router/routes.constants';
import { APP_ROUTES_ADMIN } from '@presentation/modules/admin/routes/routes-admin-constants';

@Component({
  selector: 'app-auth-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './auth-login.component.html',
  styleUrl: './auth-login.component.css'
})
export class AuthLoginComponent implements OnInit, OnDestroy {

  public form!: FormGroup;
  public isLoading = signal<boolean>(false);
  public isPasswordVisible = signal<boolean>(false);

  private destroy$ = new Subject<void>();
  private loginResult$!: Observable<ActionResult<string>>;

  constructor(
    private _router: Router,
    private _fb: FormBuilder,
    private _token: TokenService,
    private _toast: ToastService,
    private _loginUseCase: LoginUsecase,
  ) { }

  ngOnInit(): void {
    this._token.remove();
    this.initializeForm();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  togglePasswordVisibility(): void {
    this.isPasswordVisible.set(!this.isPasswordVisible());
  }

  onSubmit(): void {
    if (this.isLoading()) return;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this._toast.showError('Por favor, verifica la información ingresada.', 'Atención');
      return;
    }

    const credentials: CredentialsModel = this.form.value;

    this.isLoading.set(true);
    this.loginResult$ = this._loginUseCase.execute(credentials);
    this.loginResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<string>) => this.handle(response),
      error: () => this.isLoading.set(false),
      complete: () => this.isLoading.set(false),
    });

    this.form.reset();
  }

  private handle(response: ActionResult<string>): void {
    switch (response.statusCode) {
      case 200:
        this._toast.showSuccess(response.message, "Enhorabuena");
        this._router.navigate([`${APP_ROUTES_MODULES.MODULE_ADMINISTRATION}/${APP_ROUTES_ADMIN.ADMIN_DASHBOARD}`])
        this.form.reset();
        break;
      default:
        this._toast.showError('Ocurrió un error inesperado. Por favor, inténtalo nuevamente.');
        this.form.reset();
        break;
    }
  }

  private initializeForm(): void {
    this.form = this._fb.group({
      identification: ['', [Validators.required, Validators.maxLength(20)]],
      password: ['', [Validators.required, Validators.maxLength(255)]],
    });
  }

  get password() { return this.form.get('password')! }
  get identification() { return this.form.get('identification')! }

}
