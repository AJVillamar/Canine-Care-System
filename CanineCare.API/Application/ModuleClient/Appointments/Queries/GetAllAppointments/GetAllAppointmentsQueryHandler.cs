using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Dtos;
using Application.ModuleClient.Appointments.Mappers;
using Domain.ModuleClient.Appointments.Repositories;

namespace Application.ModuleClient.Appointments.Queries.GetAllAppointments
{
    public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, ApiResult<List<AppointmentDto>>>
    {
        private readonly IAppointmentRepository _repository;
        private readonly AppointmentMapperApp _mapper;

        public GetAllAppointmentsQueryHandler(
            IAppointmentRepository repository,
            AppointmentMapperApp mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<AppointmentDto>>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _repository.GetAllAsync();
            var appointmentsDto = appointments.Select(_mapper.ToDto).ToList();

            var message = appointmentsDto.Any()
                ? "Lista de citas"
                : "Aún no hay citas registrados";

            return new ApiResult<List<AppointmentDto>>(200, appointmentsDto, message);
        }
    }
}
