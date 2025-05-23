import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-appointmet-home',
  standalone: true,
  imports: [RouterOutlet],
  template: `<router-outlet>`,
})
export class AppointmentHomeComponent { }
