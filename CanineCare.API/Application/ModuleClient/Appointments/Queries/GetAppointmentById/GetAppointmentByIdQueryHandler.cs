using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Dtos;
using Application.ModuleClient.Appointments.Mappers;
using Domain.ModuleClient.Appointments.Repositories;
using Application.ModuleClient.Appointments.Validators;

namespace Application.ModuleClient.Appointments.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, ApiResult<AppointmentDto>>
    {
        private readonly IAppointmentRepository _repository;
        private readonly AppointmentValidator _validator;
        private readonly AppointmentMapperApp _mapper;

        public GetAppointmentByIdQueryHandler(
            IAppointmentRepository repository,
            AppointmentValidator validator,
            AppointmentMapperApp mapper )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResult<AppointmentDto>> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            await _validator.ValidateGetByIdAsync(request.Id);
            var appointment = await _repository.GetByIdAsync(request.Id);
            var appointmentDto = _mapper.ToDto(appointment);
            return new ApiResult<AppointmentDto>(200, appointmentDto, "Información de la cita.");
        }
    }
}
