import { CommonModule } from '@angular/common';
import { Observable, Subject, takeUntil } from 'rxjs';
import { FormBuilder, FormsModule, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Component, OnDestroy, OnInit, signal, ViewChild } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';

import { ToastService } from '@infrastructure/common/toast.service';
import { DialogService } from '@presentation/shared/services/dialog.service';

import { ActionResult } from '@domain/base/action-result';
import { AppointmentModel } from '@domain/models/appointments/appointment-model';
import { AppointmentUpdateComponent } from '../appointment-update/appointment-update.component';
import { AppointmentCancelUsecase } from '@domain/usecases/appointment/appointment-cancel-usecase';
import { AppointmentSearchUsecase } from '@domain/usecases/appointment/appointment-search-usecase';
import { AppointmentSearchFilterModel } from '@domain/models/appointments/appointment-searchfilter-model';

@Component({
  selector: 'app-appointment-search',
  standalone: true,
  imports: [
    FormsModule,
    CommonModule,
    MatSortModule,
    MatTableModule,
    MatTooltipModule,
    ReactiveFormsModule,
    MatProgressBarModule,
  ],
  templateUrl: './appointment-search.component.html',
  styleUrls: [
    './appointment-search.component.css',
    '../../../../../styles/table.styles.css'
  ]
})
export class AppointmentSearchComponent implements OnInit, OnDestroy {

  public isDateSearch = false;
  public searchForm!: FormGroup;
  public isLoading = signal<boolean>(false);
  public dataSource = new MatTableDataSource<AppointmentModel>();
  public displayedColumns: string[] = ['petName', 'date', 'professionalName', 'status', 'actions'];

  private destroy$ = new Subject<void>();
  private cancelResult$!: Observable<ActionResult>;
  private loadResult$!: Observable<ActionResult<AppointmentModel[]>>;

  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private _fb: FormBuilder,
    private _dialog: MatDialog,
    private _toast: ToastService,
    private _dialogService: DialogService,
    private _searchUseCase: AppointmentSearchUsecase,
    private _cancelUsecase: AppointmentCancelUsecase
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  onSearch(): void {
    const queryValue = this.query.value?.trim();
    const dateValue = this.date.value;
    const minDate = this.getDefaultDate();

    if (this.isDateSearch) {
      if (!dateValue || dateValue === minDate) {
        this._toast.showError('Por favor seleccione una fecha válida');
        return;
      }
    } else {
      if (!queryValue) {
        this._toast.showError('Por favor ingrese un nombre o cédula');
        return;
      }
    }

    this.dataSource.data = []
    const body = this.getData();

    this.isLoading.set(true)
    this.loadResult$ = this._searchUseCase.execute(body);
    this.loadResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (data: ActionResult<AppointmentModel[]>) => {
        const appointments = data.data as AppointmentModel[];
        if (appointments.length == 0) {
          this._toast.showInfo(data.message)
          return
        }

        this.dataSource.data = data.data as AppointmentModel[]
      },
      error: () => this.isLoading.set(false),
      complete: () => this.isLoading.set(false)
    });
  }

  onCancel(data: AppointmentModel): void {
    if (!data.id) {
      this._toast.showInfo('Selecciona primero un cita válida.');
      return;
    }

    if (
      data.status?.toLowerCase() === "cancelada" ||
      data.status?.toLowerCase() === "realizada"
    ) {
      this._toast.showInfo(`Lo sentimos, la cita ya está ${data.status}. Cita no disponible.`);
      return;
    }

    this._dialogService.confirm(false).subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.cancelResult$ = this._cancelUsecase.execute(data.id!);
        this.cancelResult$.pipe(takeUntil(this.destroy$)).subscribe({
          next: (response: ActionResult) => {
            this._toast.showSuccess(response.message)
            this.onSearch();
          },
        })
      }
    })
  }

  openUpdate(data: AppointmentModel) {
    if (!data.id) {
      this._toast.showInfo('Selecciona primero un cita válida.');
      return;
    }

    if (
      data.status?.toLowerCase() === "cancelada" ||
      data.status?.toLowerCase() === "realizada"
    ) {
      this._toast.showInfo(`Lo sentimos, la cita ya está ${data.status}. Cita no disponible.`);
      return;
    }

    this._dialog.open(AppointmentUpdateComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: data.id
    }).afterClosed().subscribe((respuesta: ActionResult) => this.toastClose(respuesta))
  }

  private toastClose(respuesta: ActionResult): void {
    if (respuesta == undefined) return
    if (!respuesta.statusCode) return
    if (respuesta.statusCode == 200) {
      this._toast.showSuccess(respuesta.message);
      this.onSearch()
    }
  }

  private getDefaultDate(): string {
    const date = new Date(1900, 0, 1);
    return date.toISOString().split('T')[0];
  }

  public onCheckboxChange(event: Event): void {
    this.isDateSearch = (event.target as HTMLInputElement).checked;
    if (this.isDateSearch) this.query.setValue('');
    else this.date.setValue(this.getDefaultDate());
  }


  private getData(): AppointmentSearchFilterModel {
    return {
      date: this.date.value,
      query: this.query.value
    }
  }

  private initializeForm(): void {
    this.searchForm = this._fb.group({
      query: [''],
      date: [this.getDefaultDate()]
    })
  }

  getStatusClass(status: string): string {
    const normalized = status.toLowerCase();

    switch (normalized) {
      case 'pendiente':
        return 'badge--pending';
      case 'confirmada':
        return 'badge--confirmed';
      case 'realizada':
        return 'badge--completed';
      case 'cancelada':
        return 'badge--cancelled';
      default:
        return 'badge--unknown';
    }
  }

  get query() { return this.searchForm.get('query')! }
  get date() { return this.searchForm.get('date')! }

}

