using Domain.DTOs.WorkoutDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.WorkoutService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Seed;

namespace WEBAPI.Cotrollers;
[ApiController]
[Route("[controller]")]

public class WorkoutController(IWorkoutService workoutService) : ControllerBase
{
    [HttpGet]
     [Authorize]
    public async Task<Response<List<GetWorkout>>> GetWorkoutsAsync([FromQuery]PaginationFilter WorkoutFilter)
        => await workoutService.GetWorkoutsAsync(WorkoutFilter);

    [HttpGet("{WorkoutId:int}")]
     [Authorize]
    public async Task<Response<GetWorkout>> GetWorkoutByIdAsync(int WorkoutId)
        => await workoutService.GetWorkoutByIdAsync(WorkoutId);

      [HttpPost("create")]
     [Authorize(Roles.Admin)]
    public async Task<Response<string>> CreateWorkoutAsync([FromBody]CreateWorkout Workout)
        => await workoutService.CreateWorkoutAsync(Workout);


    [HttpPut("update")]
     [Authorize(Roles.Admin)]
    public async Task<Response<string>> UpdateWorkoutAsync([FromBody]UpdateWorkout Workout)
        => await workoutService.UpdateWorkoutAsync(Workout);

    [HttpDelete("{WorkoutId:int}")]
     [Authorize(Roles.Admin)]
    public async Task<Response<bool>> DeleteWorkoutAsync(int WorkoutId)
        => await workoutService.DeleteWorkoutAsync(WorkoutId);
}
