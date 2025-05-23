using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Dtos;
using Application.ModuleClient.Appointments.Mappers;
using Domain.ModuleClient.Appointments.Repositories;

namespace Application.ModuleClient.Appointments.Queries.GetAppointmentsByWeek
{
    public class GetAppointmentsByWeekQueryHandler : IRequestHandler<GetAppointmentsByWeekQuery, ApiResult<List<AppointmentDto>>>
    {
        private readonly IAppointmentRepository _repository;
        private readonly AppointmentMapperApp _mapper;

        public GetAppointmentsByWeekQueryHandler(
            IAppointmentRepository repository, 
            AppointmentMapperApp mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<AppointmentDto>>> Handle(GetAppointmentsByWeekQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _repository.GetByWeekAsync(request.ReferenceDate);

            var result = appointments.Select(_mapper.ToDto).ToList();
            var message = result.Any()
                ? "Citas agendadas"
                : "No se encontraron citas agendadas para la semana.";

            return new ApiResult<List<AppointmentDto>>(200, result, message);
        }
    }
}
