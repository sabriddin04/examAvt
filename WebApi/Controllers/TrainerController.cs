using Domain.DTOs.TrainerDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.TrainerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Seed;

namespace WEBAPI.Cotrollers;
[ApiController]
[Route("[controller]")]
public class TrainerController(ITrainerService trainerService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<Response<List<GetTrainer>>> GetTrainersAsync([FromQuery]TrainerFilter TrainerFilter)
        => await trainerService.GetTrainersAsync(TrainerFilter);
    [Authorize]
    [HttpGet("{TrainerId:int}")]
    public async Task<Response<GetTrainer>> GetTrainerByIdAsync(int TrainerId)
        => await trainerService.GetTrainerByIdAsync(TrainerId);

    [HttpPost("create")]
    [Authorize(Roles.Admin)]
    public async Task<Response<string>> CreateTrainerAsync([FromForm]CreateTrainer Trainer)
        => await trainerService.CreateTrainerAsync(Trainer);


    [HttpPut("update")]
     [Authorize(Roles.Admin)]
    public async Task<Response<string>> UpdateTrainerAsync([FromForm]UpdateTrainer Trainer)
        => await trainerService.UpdateTrainerAsync(Trainer);

    [HttpDelete("{TrainerId:int}")]
     [Authorize(Roles.Admin)]
    public async Task<Response<bool>> DeleteTrainerAsync(int TrainerId)
        => await trainerService.DeleteTrainerAsync(TrainerId);
}
