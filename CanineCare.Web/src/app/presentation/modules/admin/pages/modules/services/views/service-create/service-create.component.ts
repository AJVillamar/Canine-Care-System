import { Observable, Subject, takeUntil } from 'rxjs';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { ActionResult } from '@domain/base/action-result';
import { ServiceModel } from '@domain/models/appointments/service-model';
import { ServiceDetailModel } from '@domain/models/appointments/service-detail-model';
import { ServiceCreateUsecase } from '@domain/usecases/services/service-create-usecase';
import { ServiceTypeGetAllUsecase } from '@domain/usecases/services/service-type-getall-usecase';

import { ToastService } from '@infrastructure/common/toast.service';
import { DialogService } from '@presentation/shared/services/dialog.service';

@Component({
  selector: 'app-service-create',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatTableModule,
  ],
  templateUrl: './service-create.component.html',
  styleUrls: [
    './service-create.component.css',
    '../../../../../styles/table.styles.css'
  ]
})
export class ServiceCreateComponent implements OnInit, OnDestroy {

  public labels = {
    name: 'Nombre de la actividad',
    desc: 'Descripción de la actividad'
  };

  public dataSource = new MatTableDataSource<ServiceDetailModel>();
  public displayedColumns: string[] = ['title', 'description', 'actions'];

  public form!: FormGroup;
  public activityForm!: FormGroup;
  public isSubmitting = signal<boolean>(false);

  public servicesType: string[] = []
  public isLoadingDropdowns = signal<boolean>(false);

  private destroy$ = new Subject<void>;
  private createResult$!: Observable<ActionResult>;
  private getAllTypesResult$!: Observable<ActionResult<string[]>>;

  constructor(
    private _fb: FormBuilder,
    private _toast: ToastService,
    private _dialogService: DialogService,
    private _createUseCase: ServiceCreateUsecase,
    private _getAllTypesUseCase: ServiceTypeGetAllUsecase
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

    if (this.dataSource.data.length === 0) {
      this._toast.showError('Debe agregar al menos un detalle al servicio.');
      return;
    }

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this._toast.showError('Por favor, verifica la información ingresada.');
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

  addDetail(): void {
    if (this.isSubmitting()) return;

    if (this.activityForm.invalid) {
      this.activityForm.markAllAsTouched();
      this._toast.showError('Por favor, verifica la información ingresada.');
      return;
    }

    const detail: ServiceDetailModel = {
      title: this.activityName.value,
      description: this.activityDescription.value
    };

    this.dataSource.data = [...this.dataSource.data, detail];
    this.activityForm.reset();
    this._toast.showSuccess('Detalle agregado correctamente.');
  }

  removeDetail(index: number): void {
    const currentData = [...this.dataSource.data];
    currentData.splice(index, 1);
    this.dataSource.data = currentData;
    this._toast.showSuccess('Detalle eliminado correctamente.');
  }

  onSelectType(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    const type = selectElement.value;

    switch (type) {
      case 'Baño Normal':
      case 'Baño Medicado':
        this.labels = { name: 'Nombre del medicamento', desc: 'Indicaciones' };
        break;
      case 'Peluquería Canina':
        this.labels = { name: 'Servicio realizado', desc: 'Observaciones' };
        break;
      case 'Desparasitación':
        this.labels = { name: 'Nombre del antiparasitario', desc: 'Instrucciones de aplicación' };
        break;
      default:
        this.labels = { name: 'Nombre de la actividad', desc: 'Descripción de la actividad' };
        break;
    }
  }

  private getData(): ServiceModel {
    return {
      name: this.name.value,
      description: this.description.value,
      type: this.type.value,
      actions: this.dataSource.data
    }
  }

  private handle(response: ActionResult): void {
    switch (response.statusCode) {
      case 201:
        this._toast.showSuccess(response.message);
        this.form.reset();
        this.type.setValue('');
        this.activityForm.reset();
        this.dataSource.data = [];
        break;
      default:
        this._toast.showError('Ocurrió un error inesperado. Por favor, inténtalo nuevamente.');
        this.form.reset();
        break;
    }
  }

  private initializeForm(): void {
    this.form = this._fb.group({
      name: ['', [Validators.required, Validators.maxLength(500)]],
      description: ['', [Validators.required]],
      type: ['', [Validators.required]],
    });

    this.activityForm = this._fb.group({
      activityName: ['', Validators.required],
      activityDescription: ['', Validators.required]
    });

    this.loadOptionsSelect();
  }

  private loadOptionsSelect() {
    this.isLoadingDropdowns.set(true);
    this.getAllTypesResult$ = this._getAllTypesUseCase.execute();
    this.getAllTypesResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<string[]>) => this.servicesType = response.data as string[],
      error: () => this.isLoadingDropdowns.set(false),
      complete: () => this.isLoadingDropdowns.set(false)
    });
  }

  get name() { return this.form.get('name')! }
  get description() { return this.form.get('description')! }
  get type() { return this.form.get('type')! }

  get activityName() { return this.activityForm.get('activityName')! }
  get activityDescription() { return this.activityForm.get('activityDescription')! }

}
