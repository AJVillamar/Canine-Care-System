import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subject, takeUntil } from 'rxjs';
import { AfterViewInit, Component, OnDestroy, OnInit, signal, ViewChild } from '@angular/core';

import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';

import { ToastService } from '@infrastructure/common/toast.service';
import { ProfessionalDetailComponent } from '../professional-detail/professional-detail.component';

import { ActionResult } from '@domain/base/action-result';
import { ProfessionalModel } from '@domain/models/people/professional-model';
import { ProfessionalGetAllUsecase } from '@domain/usecases/professional/professional-getall-usecase';

@Component({
  selector: 'app-professional-list',
  standalone: true,
  imports: [
    CommonModule,
    MatSortModule,
    MatTableModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatProgressBarModule,
  ],
  templateUrl: './professional-list.component.html',
  styleUrls: [
    './professional-list.component.css',
    '../../../../../styles/table.styles.css'
  ]
})
export class ProfessionalListComponent implements OnInit, AfterViewInit, OnDestroy {

  public isLoading = signal<boolean>(false);

  public dataSource = new MatTableDataSource<ProfessionalModel>();
  public displayedColumns: string[] = ['identification', 'lastName', 'firstName', 'email', 'actions'];

  private destroy$ = new Subject<void>();
  private loadResult$!: Observable<ActionResult<ProfessionalModel[]>>;

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private _dialog: MatDialog,
    private _toast: ToastService,
    private _getAllUseCase: ProfessionalGetAllUsecase,
  ) { }

  ngOnInit(): void {
    this.loadProfessionals()
  }

  ngOnDestroy(): void {
    this.destroy$.next()
    this.destroy$.complete()
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    if (this.dataSource.data.length > 0) {
      this.paginator._intl.itemsPerPageLabel = "Items por Página ";
    }
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  openEdit(id: string) {
    this._dialog.open(ProfessionalDetailComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: id
    })
  }

  private loadProfessionals() {
    this.isLoading.set(true)
    this.loadResult$ = this._getAllUseCase.execute()
    this.loadResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (data: ActionResult<ProfessionalModel[]>) => this.dataSource.data = data.data as ProfessionalModel[],
      error: () => this.isLoading.set(false),
      complete: () => this.isLoading.set(false)
    })
  }

}
