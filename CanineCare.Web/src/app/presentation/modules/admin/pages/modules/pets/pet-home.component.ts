import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-pet-home',
  standalone: true,
  imports: [RouterOutlet],
  template: `<router-outlet>`,
})
export class PetHomeComponent { }
