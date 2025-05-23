import { provideToastr } from 'ngx-toastr';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

import { routes } from '../presentation/router/app.routes';
import { TokenInterceptor } from '@infrastructure/interceptors/token.interceptor';
import { ErrorInterceptor } from '@infrastructure/interceptors/error.interceptor';
import { AuthRepositoryImplService } from '@infrastructure/repositories/auth/driver-adapter/auth-repository-impl.service';
import { PetRepositoryImplService } from '@infrastructure/repositories/clients/pet/driver-adapter/pet-repository-impl.service';
import { OwnerRepositoryImplService } from '@infrastructure/repositories/people/owner/driver-adapter/owner-repository-impl.service';
import { ServiceRepositoryImplService } from '@infrastructure/repositories/clients/services/driver-adapter/service-repository-impl.service';
import { AppointmentRepositoryImplService } from '@infrastructure/repositories/clients/appointments/driver-adapter/appointment-repository-impl.service';
import { ProfessionalRepositoryImplService } from '@infrastructure/repositories/people/professional/driver-adapter/professional-repository-impl.service';

import { PetRepository } from '@domain/repositories/pet/pet-repository';
import { OwnerRepository } from '@domain/repositories/people/owner-repository';
import { AuthRepository } from '@domain/repositories/authentication/auth-repository';
import { ServiceRepository } from '@domain/repositories/appointment/service-repository';
import { ProfessionalRepository } from '@domain/repositories/people/professional-repository';
import { AppointmentRepository } from '@domain/repositories/appointment/appointment-repository';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([TokenInterceptor, ErrorInterceptor])),
    provideAnimationsAsync(),
    provideToastr({
      timeOut: 5000,
      preventDuplicates: true,
      progressBar: true,
      closeButton: true,
      positionClass: 'toast-top-right'
    }),
    { provide: AuthRepository, useClass: AuthRepositoryImplService },
    { provide: OwnerRepository, useClass: OwnerRepositoryImplService },
    { provide: ProfessionalRepository, useClass: ProfessionalRepositoryImplService },
    { provide: PetRepository, useClass: PetRepositoryImplService },
    { provide: ServiceRepository, useClass: ServiceRepositoryImplService },
    { provide: AppointmentRepository, useClass: AppointmentRepositoryImplService },
  ]
};
