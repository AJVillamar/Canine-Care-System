import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

import { PetExtraInfoModel } from '@domain/models/pet/pet-extrainfo-model';

@Component({
  selector: 'app-pet-detail-additional-info',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './pet-detail-additional-info.component.html',
  styleUrl: './pet-detail-additional-info.component.css'
})
export class PetDetailAdditionalInfoComponent implements OnInit {

  public additionalInfoForm!: FormGroup;

  constructor(
    private _fb: FormBuilder,
    private _dialogRef: MatDialogRef<PetDetailAdditionalInfoComponent>,
    @Inject(MAT_DIALOG_DATA) public data: PetExtraInfoModel
  ) { }

  ngOnInit(): void {
    this.initializeForm(this.data);
  }

  onCancel(): void {
    this._dialogRef.close(false);
  }


  private initializeForm(data: PetExtraInfoModel): void {
    this.additionalInfoForm = this._fb.group({
      allergies: [data.allergies],
      preExistingConditions: [data.preExistingConditions],
      specialCareInstructions: [data.specialCareInstructions],
      feedingNotes: [data.feedingNotes],
    });
  }

  get allergies() { return this.additionalInfoForm.get('allergies')! }
  get preExistingConditions() { return this.additionalInfoForm.get('preExistingConditions')! }
  get specialCareInstructions() { return this.additionalInfoForm.get('specialCareInstructions')! }
  get feedingNotes() { return this.additionalInfoForm.get('feedingNotes')! }

}
