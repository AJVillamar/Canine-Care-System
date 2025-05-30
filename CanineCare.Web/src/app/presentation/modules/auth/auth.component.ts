import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    RouterOutlet,
  ],
  template: `
    <div style="background-color: transparent;">
        <router-outlet />
    </div>
  `,
})
export class AuthComponent { }
