using MediatR;
using Shared.Responses;
using Application.ModuleClient.Appointments.Dtos;
using Application.ModuleClient.Appointments.Mappers;
using Domain.ModuleClient.Appointments.Repositories;

namespace Application.ModuleClient.Appointments.Queries.SearchAppointments
{
    public class SearchAppointmentsQueryHandler : IRequestHandler<SearchAppointmentsQuery, ApiResult<List<AppointmentDto>>>
    {
        private readonly IAppointmentRepository _repository;
        private readonly AppointmentMapperApp _mapper;

        public SearchAppointmentsQueryHandler(
            IAppointmentRepository repository,
            AppointmentMapperApp mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<List<AppointmentDto>>> Handle(SearchAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.ToFilter(request);
            var appointments = await _repository.SearchAsync(filter);

            var result = appointments.Select(_mapper.ToDto).ToList();
            var message = result.Any()
                ? "Citas encontradas"
                : "No se encontraron citas con los criterios especificados.";

            return new ApiResult<List<AppointmentDto>>(200, result, message);
        }
    }
}
