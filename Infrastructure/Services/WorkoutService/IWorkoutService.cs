using Domain.DTOs.WorkoutDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.WorkoutService;

public interface IWorkoutService
{
    Task<PagedResponse<List<GetWorkout>>> GetWorkoutsAsync(PaginationFilter filter);
    Task<Response<GetWorkout>> GetWorkoutByIdAsync(int id);
    Task<Response<string>> CreateWorkoutAsync(CreateWorkout Workout);
    Task<Response<string>> UpdateWorkoutAsync(UpdateWorkout Workout);
    Task<Response<bool>> DeleteWorkoutAsync(int id);
}
