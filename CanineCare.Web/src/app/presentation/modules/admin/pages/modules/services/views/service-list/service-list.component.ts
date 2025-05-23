import { CommonModule } from '@angular/common';
import { Observable, Subject, takeUntil } from 'rxjs';
import { AfterViewInit, Component, OnDestroy, OnInit, signal, ViewChild } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';

import { ActionResult } from '@domain/base/action-result';
import { ServiceModel } from '@domain/models/appointments/service-model';
import { ServiceDetailComponent } from '../service-detail/service-detail.component';
import { ServiceGetAllUsecase } from '@domain/usecases/services/service-getall-usecase';

@Component({
  selector: 'app-service-list',
  standalone: true,
  imports: [
    CommonModule,
    MatSortModule,
    MatTableModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatProgressBarModule,
  ],
  templateUrl: './service-list.component.html',
  styleUrls: [
    './service-list.component.css',
    '../../../../../styles/table.styles.css'
  ]
})
export class ServiceListComponent implements OnInit, AfterViewInit, OnDestroy {

  public isLoading = signal<boolean>(false); 
  public dataSource = new MatTableDataSource<ServiceModel>();
  public displayedColumns: string[] = ['name', 'type', 'actions'];

  private destroy$ = new Subject<void>();
  private loadResult$!: Observable<ActionResult<ServiceModel[]>>;

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private _dialog: MatDialog,
    private _getAllUseCase: ServiceGetAllUsecase,
  ) { }

  ngOnInit(): void {
    this.loadServices();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openDetail(id: string) {
    this._dialog.open(ServiceDetailComponent, {
      autoFocus: false,
      disableClose: true,
      width: '550px',
      data: id
    })
  }

  private loadServices() {
    this.isLoading.set(true)
    this.loadResult$ = this._getAllUseCase.execute();
    this.loadResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (data: ActionResult<ServiceModel[]>) => this.dataSource.data = data.data as ServiceModel[],
      error: () => this.isLoading.set(false),
      complete: () => this.isLoading.set(false)
    });
  }

}
