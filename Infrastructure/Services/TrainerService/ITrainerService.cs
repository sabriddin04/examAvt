using Domain.DTOs.TrainerDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.TrainerService;

public interface ITrainerService
{
    Task<PagedResponse<List<GetTrainer>>> GetTrainersAsync(TrainerFilter filter);
    Task<Response<GetTrainer>> GetTrainerByIdAsync(int id);
    Task<Response<string>> CreateTrainerAsync(CreateTrainer Trainer);
    Task<Response<string>> UpdateTrainerAsync(UpdateTrainer Trainer);
    Task<Response<bool>> DeleteTrainerAsync(int id);
}
