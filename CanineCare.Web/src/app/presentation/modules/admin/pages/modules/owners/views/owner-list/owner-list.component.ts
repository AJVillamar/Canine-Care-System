import { Router } from '@angular/router';
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
import { OwnerModel } from '@domain/models/people/owner-model';
import { OwnerGetAllUsecase } from '@domain/usecases/owner/owner-getall-usecase';

import { APP_ROUTES_MODULES } from '@presentation/router/routes.constants';
import { OwnerDetailComponent } from '../owner-detail/owner-detail.component';
import { APP_ROUTES_ADMIN } from '@presentation/modules/admin/routes/routes-admin-constants';

@Component({
  selector: 'app-owner-list',
  standalone: true,
  imports: [
    CommonModule,
    MatSortModule,
    MatTableModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatProgressBarModule,
  ],
  templateUrl: './owner-list.component.html',
  styleUrls: [
    './owner-list.component.css',
    '../../../../../styles/table.styles.css'
  ]
})
export class OwnerListComponent implements OnInit, AfterViewInit, OnDestroy {

  public isLoading = signal<boolean>(false);
  public dataSource = new MatTableDataSource<OwnerModel>();
  public displayedColumns: string[] = ['identification', 'lastName', 'firstName', 'phone', 'actions'];

  private destroy$ = new Subject<void>();
  private loadResult$!: Observable<ActionResult<OwnerModel[]>>;

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private _router: Router,
    private _dialog: MatDialog,
    private _getAllUseCase: OwnerGetAllUsecase,
  ) { }

  ngOnInit(): void {
    this.loadOwners();
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
    this._dialog.open(OwnerDetailComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: id
    })
  }

  goToEdit(identification: string): void {
    this._router.navigate([
      APP_ROUTES_MODULES.MODULE_ADMINISTRATION,
      APP_ROUTES_ADMIN.ADMIN_OWNER,
      "edit"
    ], { queryParams: { identification } });
  }

  private loadOwners() {
    this.isLoading.set(true)
    this.loadResult$ = this._getAllUseCase.execute();
    this.loadResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (data: ActionResult<OwnerModel[]>) => this.dataSource.data = data.data as OwnerModel[],
      error: () => this.isLoading.set(false),
      complete: () => this.isLoading.set(false)
    });
  }

}
