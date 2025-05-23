import { Observable, map, shareReplay } from 'rxjs';
import { Component, inject, OnInit } from '@angular/core';
import { AsyncPipe, CommonModule } from '@angular/common';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Router, RouterLink, RouterOutlet, Routes } from '@angular/router';

import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';

import { LoggedUserModel } from '@domain/models/authentication/logged-user-model';

import { ToastService } from '@infrastructure/common/toast.service';
import { TokenService } from '@infrastructure/common/token.service';

import { AdminPages } from '../../routes/admin-pages.routes';
import { APP_ROUTES_MODULES } from '@presentation/router/routes.constants';
import { APP_ROUTES_AUTH } from '@presentation/modules/auth/routes/routes-auth-constants';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  standalone: true,
  imports: [
    AsyncPipe,
    RouterOutlet,
    RouterLink,
    CommonModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    MatToolbarModule,
    MatSidenavModule,
    MatTooltipModule
  ]
})
export class HomeComponent implements OnInit {

  public menuItems: Routes = [];
  public user!: LoggedUserModel | null;
  public currentYear: number = new Date().getFullYear();
  public pathMain = APP_ROUTES_MODULES.MODULE_ADMINISTRATION;

  private _router = inject(Router);
  private _toast = inject(ToastService);
  private _token = inject(TokenService);
  private _breakpointObserver = inject(BreakpointObserver);

  isHandset$: Observable<boolean> = this._breakpointObserver.observe(Breakpoints.Handset)
    .pipe(map(result => result.matches), shareReplay());

  ngOnInit(): void {
    this.user = this.getValidatedUser();
    if (!this.user) return;
    this.menuItems = this.getFilteredRoutes();
  }

  exitSession() {
    this._token.remove();
    this._toast.showSuccess("Sesión cerrada exitosamente");
    this._router.navigate([`${APP_ROUTES_MODULES.MODULE_AUTHENTICATION}/${APP_ROUTES_AUTH.AUTH_LOGIN}`]);
  }

  isActivePath(...routePaths: string[]): boolean {
    const fullPath = `/${APP_ROUTES_MODULES.MODULE_ADMINISTRATION}/${routePaths.join('/')}`;
    return this._router.url.startsWith(fullPath);
  }


  navigateToBreadcrumb(path: string): void {
    this._router.navigate([path]);
  }

  getBreadcrumbItems(): { label: string, url: string }[] {
    const pathSegments = this._router.url.split('/').filter(Boolean);
    const items: { label: string, url: string }[] = [];

    let accumulatedPath = '';
    let currentRoutes: Routes = AdminPages.flatMap(route => route.children ?? []);

    pathSegments.slice(1).forEach(segment => {
      const route = currentRoutes.find(r => r.path === segment);

      accumulatedPath += `/${segment}`;
      const label = route?.data?.['label'] ?? (route as any)?.label ?? segment;

      items.push({ label, url: `/${pathSegments[0]}${accumulatedPath}` });

      currentRoutes = route?.data?.['children'] ?? [];
    });

    return items;
  }

  private getFilteredRoutes(): Routes {
    const userRoles = Array.isArray(this.user!.role)
      ? this.user!.role.map(role => role.trim().toLowerCase())
      : [this.user!.role.trim().toLowerCase()];

    return AdminPages
      .map(route => {
        if (!route.children) return [];
        const visibleChildren = route.children.map(child => {
          const filteredChildren = (child.data?.['children'] || []).filter((sub: any) => !sub.hidden);
          return {
            ...child,
            data: {
              ...child.data,
              children: filteredChildren
            }
          };
        });

        return visibleChildren;
      })
      .flat()
      .filter(route => route && route.path && !route.path.includes(':'))
      .filter(route => {
        const data = route.data as { roles?: string[] };
        const routeRoles = data?.roles?.map((r: string) => r.toLowerCase()) || [];
        return userRoles.some(userRole => routeRoles.includes(userRole));
      });;
  }

  private getValidatedUser(): LoggedUserModel | null {
    const user = this._token.getLoggedUser();
    const isInvalidUser = !user || !user.id || !user.role;

    if (isInvalidUser) {
      this._toast.showError('No se encontró información del usuario. Por favor, inicia sesión.');
      this._router.navigate([
        APP_ROUTES_MODULES.MODULE_AUTHENTICATION,
        APP_ROUTES_AUTH.AUTH_LOGIN
      ]);
      return null;
    }
    return user;
  }

}
