using Domain.DTOs.ClassSchduleDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.ClassSchduleService;

public interface IClassScheduleService
{
    Task<PagedResponse<List<GetClassSchedule>>> GetClassSchedulesAsync(PaginationFilter filter);
    Task<Response<GetClassSchedule>> GetClassScheduleByIdAsync(int id);
    Task<Response<string>> CreateClassScheduleAsync(CreateClassSchedule ClassSchedule);
    Task<Response<string>> UpdateClassScheduleAsync(UpdateClassSchedule ClassSchedule);
    Task<Response<bool>> DeleteClassScheduleAsync(int id);
}
