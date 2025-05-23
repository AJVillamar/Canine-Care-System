using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Mappers;
using Domain.ModuleClient.Appointments.Repositories;
using Application.ModuleClient.Appointments.Validators;

namespace Application.ModuleClient.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, ApiResult>
    {
        private readonly IAppointmentRepository _repository;
        private readonly AppointmentValidator _validator;
        private readonly AppointmentMapperApp _mapper;

        public UpdateAppointmentCommandHandler(
            IAppointmentRepository repository,
            AppointmentValidator validator,
            AppointmentMapperApp mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }


        public async Task<ApiResult> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateUpdateAsync(request);
            var appointment = await _repository.GetByIdAsync(request.Id);
            var appointmentDomain = _mapper.ToDomain(request, appointment!);
            await _repository.UpdateAsync(appointmentDomain);
            return new ApiResult(200, "Cita actualizada exitosamente.");
        }
    }
}
