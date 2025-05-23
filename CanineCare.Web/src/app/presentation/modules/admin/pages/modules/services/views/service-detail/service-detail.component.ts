import { Observable, Subject, takeUntil } from 'rxjs';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Component, OnInit, OnDestroy, signal, Inject } from '@angular/core';

import { ActionResult } from '@domain/base/action-result';
import { ServiceModel } from '@domain/models/appointments/service-model';
import { ServiceGetByIdUsecase } from '@domain/usecases/services/service-getbyid-usecase';
import { ServiceDetailModel } from '@domain/models/appointments/service-detail-model';

@Component({
  selector: 'app-service-detail',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatTableModule
  ],
  templateUrl: './service-detail.component.html',
  styleUrls: [
    './service-detail.component.css',
    '../../../../../styles/table.styles.css'
  ]
})
export class ServiceDetailComponent implements OnInit, OnDestroy {

  public form!: FormGroup;
  public isLoadingData = signal<boolean>(false);

  public dataSource = new MatTableDataSource<ServiceDetailModel>();
  public displayedColumns: string[] = ['title', 'description'];

  private destroy$ = new Subject<void>();
  private getByIdResult$!: Observable<ActionResult<ServiceModel>>;

  constructor(
    private _fb: FormBuilder,
    private _getByIdUseCase: ServiceGetByIdUsecase,
    private _dialogRef: MatDialogRef<ServiceDetailComponent>,
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

  private initializeForm(): void {
    this.form = this._fb.group({
      name: [''],
      description: [''],
      type: ['']
    });
  }

  private loadData(id: string): void {
    this.isLoadingData.set(true);
    this.getByIdResult$ = this._getByIdUseCase.execute(id);
    this.getByIdResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<ServiceModel>) => this.setData(response.data as ServiceModel),
      error: () => this.isLoadingData.set(false),
      complete: () => this.isLoadingData.set(false)
    });
  }

  private setData(data: ServiceModel): void {
    this.form.patchValue({
      name: data.name,
      description: data.description,
      type: data.type
    });

    this.dataSource.data = data.actions ?? []
  }

  get name() { return this.form.get('name')! }
  get description() { return this.form.get('description')! }
  get type() { return this.form.get('type')! }

}
