using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Dtos;
using Domain.ModuleClient.Appointments.Repositories;

namespace Application.ModuleClient.Appointments.Queries.GetAppointmentHours
{
    public class GetAppointmentHoursQueryHandler : IRequestHandler<GetAppointmentHoursQuery, ApiResult<List<AppointmentHourDto>>>
    {
        private readonly IAppointmentRepository _repository;

        public GetAppointmentHoursQueryHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ApiResult<List<AppointmentHourDto>>> Handle(GetAppointmentHoursQuery request, CancellationToken cancellationToken)
        {
            var hours = await _repository.GetAppointmentHoursAsync();

            var dtoList = hours.Select(h => new AppointmentHourDto { Time = h }).ToList();

            var message = dtoList.Any()
                ? "Horas disponibles para agendar"
                : "No hay horas configuradas";

            return new ApiResult<List<AppointmentHourDto>>(200, dtoList, message);
        }
    }
}
