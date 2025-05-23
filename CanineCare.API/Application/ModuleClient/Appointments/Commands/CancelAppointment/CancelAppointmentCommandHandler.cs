using MediatR;
using Shared.Responses;
using Domain.ModuleClient.Appointments.Repositories;
using Application.ModuleClient.Appointments.Validators;

namespace Application.ModuleClient.Appointments.Commands.CancelAppointment
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, ApiResult>
    {
        private readonly IAppointmentRepository _repository;        
        private readonly AppointmentValidator _validator;

        public CancelAppointmentCommandHandler(
            IAppointmentRepository repository, 
            AppointmentValidator validator )
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<ApiResult> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateGetByIdAsync(request.Id);
            var appointment = await _repository.GetByIdAsync(request.Id);
            appointment.Cancel();
            await _repository.CancelAsync(appointment);
            return new ApiResult(200, "Cita cancelada exitosamente.");
        }
    }
}
