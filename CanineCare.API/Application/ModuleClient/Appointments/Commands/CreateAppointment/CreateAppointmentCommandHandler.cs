using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Mappers;
using Domain.ModuleClient.Appointments.Repositories;
using Application.ModuleClient.Appointments.Validators;

namespace Application.ModuleClient.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, ApiResult>
    {
        private readonly IAppointmentRepository _repository;
        private readonly AppointmentValidator _validator;
        private readonly AppointmentMapperApp _mapper;

        public CreateAppointmentCommandHandler(
            IAppointmentRepository repository,
            AppointmentValidator validator,
            AppointmentMapperApp mapper )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            await _validator.ValidateCreateAsync(request);
            var appointment = _mapper.ToDomain(request);
            await _repository.AddAsync(appointment);
            return new ApiResult(201, "Cita agendada exitosamente.");
        }
    }
}
