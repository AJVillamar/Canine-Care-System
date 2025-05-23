import { CommonModule } from '@angular/common';
import { Observable, Subject, takeUntil } from 'rxjs';
import { AfterViewInit, Component, OnDestroy, OnInit, signal, ViewChild } from '@angular/core';

import { MatDialog } from '@angular/material/dialog';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';

import { PetModel } from '@domain/models/pet/pet-model';
import { ActionResult } from '@domain/base/action-result';
import { PetGetAllUsecase } from '@domain/usecases/pet/pet-getall-usecase';

import { PetDetailBasicInfoComponent } from '../pet-detail-basic-info/pet-detail-basic-info.component';

@Component({
  selector: 'app-pet-list',
  standalone: true,
  imports: [
    CommonModule,
    MatSortModule,
    MatTableModule,
    MatTooltipModule,
    MatPaginatorModule,
    MatProgressBarModule,
  ],
  templateUrl: './pet-list.component.html',
  styleUrls: [
    './pet-list.component.css',
    '../../../../../styles/table.styles.css'
  ]
})
export class PetListComponent implements OnInit, AfterViewInit, OnDestroy {

  public isLoading = signal<boolean>(false);
  public dataSource = new MatTableDataSource<PetModel>();
  public displayedColumns: string[] = ['name', 'breed', 'client', 'actions'];

  private destroy$ = new Subject<void>();
  private loadResult$!: Observable<ActionResult<PetModel[]>>;

  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private _dialog: MatDialog,
    private _getAllUseCase: PetGetAllUsecase,
  ) { }

  ngOnInit(): void {
    this.loadPets();
    this.setFilterPredicate();
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
    const filterValue = (event.target as HTMLInputElement).value.trim().toLowerCase();
    this.dataSource.filter = filterValue;

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  private setFilterPredicate(): void {
    this.dataSource.filterPredicate = (data: PetModel, filter: string): boolean => {
      const name = data.name?.toLowerCase() || '';
      const ownerFirstName = data.owner?.firstName?.toLowerCase() || '';
      const ownerLastName = data.owner?.lastName?.toLowerCase() || '';

      return name.includes(filter) || ownerFirstName.includes(filter) || ownerLastName.includes(filter);
    };
  }

  openDetailPet(id: string) {
    this._dialog.open(PetDetailBasicInfoComponent, {
      autoFocus: false,
      disableClose: true,
      width: '500px',
      data: id
    })
  }

  private loadPets() {
    this.isLoading.set(true)
    this.loadResult$ = this._getAllUseCase.execute();
    this.loadResult$.pipe(takeUntil(this.destroy$)).subscribe({
      next: (data: ActionResult<PetModel[]>) => this.dataSource.data = data.data as PetModel[],
      error: () => this.isLoading.set(false),
      complete: () => this.isLoading.set(false)
    });
  }

}
