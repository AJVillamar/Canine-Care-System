import { registerLocaleData } from '@angular/common';
import localeEs from '@angular/common/locales/es';

registerLocaleData(localeEs);
import { CommonModule } from '@angular/common';
import { Observable, Subject, takeUntil } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Component, OnDestroy, OnInit, signal } from '@angular/core';

import { ToastService } from '@infrastructure/common/toast.service';
import { AppointmentDetailComponent } from '../../../appointments/views/appointment-detail/appointment-detail.component';

import { ActionResult } from '@domain/base/action-result';
import { AppointmentModel } from '@domain/models/appointments/appointment-model';
import { AppointmentHourModel } from '@domain/models/appointments/appointment-hour-model';
import { AppointmentGetHoursUsecase } from '@domain/usecases/appointment/appointment-gethours-usecase';
import { AppointmentGetByWeekUsecase } from '@domain/usecases/appointment/appointment-getbyweek-usecase';

@Component({
  selector: 'app-dashboard-overview',
  standalone: true,
  imports: [
    CommonModule,
    MatTooltipModule
  ],
  templateUrl: './dashboard-overview.component.html',
  styleUrl: './dashboard-overview.component.css'
})
export class DashboardOverviewComponent implements OnInit, OnDestroy {

  public weekDates: Date[] = [];
  public currentDate = new Date();
  public hours: AppointmentHourModel[] = [];
  public appointments: AppointmentModel[] = [];
  public isLoading = signal<boolean>(false);
  public isSearch = signal<boolean>(false);

  private destroy$ = new Subject<void>;
  private getWeekResult$!: Observable<ActionResult<AppointmentModel[]>>;
  private getAllHourResult$!: Observable<ActionResult<AppointmentHourModel[]>>;

  constructor(
    private _dialog: MatDialog,
    private _toast: ToastService,
    private _getAllHoursUseCase: AppointmentGetHoursUsecase,
    private _getAllWekkAppointmetUseCase: AppointmentGetByWeekUsecase
  ) { }

  ngOnInit(): void {
    this.updateWeekDates();
    this.loadHours();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadHours(): void {
    this.isLoading.set(true);
    this.getAllHourResult$ = this._getAllHoursUseCase.execute();
    this.getAllHourResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<AppointmentHourModel[]>) => this.hours = response.data as AppointmentHourModel[],
      error: () => this.isLoading.set(false),
      complete: () => this.isLoading.set(false),
    });
  }

  getAppointmentWeek(): void {
    this.isSearch.set(true);
    const wednesday = this.weekDates[2];
    this.getWeekResult$ = this._getAllWekkAppointmetUseCase.execute(wednesday);
    this.getWeekResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (response: ActionResult<AppointmentHourModel[]>) => this.appointments = response.data as AppointmentModel[],
      error: () => this.isSearch.set(false),
      complete: () => this.isSearch.set(false),
    });
  }

  private updateWeekDates(): void {
    const day = this.currentDate.getDay();
    const diff = day === 0 ? -6 : 1 - day;

    const monday = new Date(this.currentDate);
    monday.setDate(this.currentDate.getDate() + diff);

    this.weekDates = [];

    for (let i = 0; i < 7; i++) {
      const date = new Date(monday);
      date.setDate(monday.getDate() + i);
      this.weekDates.push(date);
    }
    this.getAppointmentWeek();
  }

  public goToPreviousWeek(): void {
    this.currentDate.setDate(this.currentDate.getDate() - 7);
    this.updateWeekDates();
  }

  public goToNextWeek(): void {
    this.currentDate.setDate(this.currentDate.getDate() + 7);
    this.updateWeekDates();
  }

  public goToCurrentWeek(): void {
    this.currentDate = new Date();
    this.updateWeekDates();
  }

  openDetail(id: string) {
    if (!id) {
      this._toast.showInfo('Selecciona primero una mascota.');
      return;
    }

    this._dialog.open(AppointmentDetailComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: id
    })
  }

  getAppointmentByDateAndTime(date: Date, time: string): AppointmentModel | undefined {
    const formattedDate = date.toISOString().split('T')[0];

    return this.appointments.find(app => {
      if (!app.date || !app.time) return false;
      const appDateStr = new Date(app.date).toISOString().split('T')[0];
      const match = appDateStr === formattedDate && app.time === time;
      return match;
    });
  }

}
