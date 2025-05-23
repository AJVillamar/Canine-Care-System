import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-dashboard-home',
  standalone: true,
  imports: [RouterOutlet],
  template: `<router-outlet>`,
})
export class DashboardHomeComponent {

}
