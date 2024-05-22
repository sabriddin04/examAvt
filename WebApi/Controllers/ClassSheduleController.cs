using Domain.Responses;
using Domain.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.DTOs.ClassSchduleDto;
using Infrastructure.Services.ClassSchduleService;
using Infrastructure.Seed;
namespace WEBAPI.Cotrollers;
[ApiController]
[Route("[controller]")]

public class ClassScheduleController(IClassScheduleService classScheduleService)
{
    [HttpGet]
    [Authorize]
    public async Task<Response<List<GetClassSchedule>>> GetClassSchedulesAsync([FromQuery]PaginationFilter filter)
        => await classScheduleService.GetClassSchedulesAsync(filter);

    [HttpGet("{ClassScheduleId:int}")]
    [Authorize]
    public async Task<Response<GetClassSchedule>> GetClassScheduleByIdAsync(int ClassScheduleId)
        => await classScheduleService.GetClassScheduleByIdAsync(ClassScheduleId);

    [HttpPost("create")]
    [Authorize(Roles.Admin)]

    public async Task<Response<string>> CreateClassScheduleAsync([FromBody]CreateClassSchedule ClassSchedule)
        => await classScheduleService.CreateClassScheduleAsync(ClassSchedule);

    
    [HttpPut("update")]
    [Authorize(Roles.Admin)]
    public async Task<Response<string>> UpdateClassScheduleAsync([FromBody]UpdateClassSchedule ClassSchedule)
        => await classScheduleService.UpdateClassScheduleAsync(ClassSchedule);

    [HttpDelete("{ClassScheduleId:int}")]
    [Authorize(Roles.Admin)]
    public async Task<Response<bool>> DeleteClassScheduleAsync(int ClassScheduleId)
        => await classScheduleService.DeleteClassScheduleAsync(ClassScheduleId);
}
