using AutoMapper;
using Domain.Responses;
using Domain.DTOs.WorkoutDto;
using Domain.Entities;
using Infrastructure.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Domain.Filters;
using System.Data.Common;

namespace Infrastructure.Services.WorkoutService;

public class WorkoutService : IWorkoutService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public WorkoutService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> CreateWorkoutAsync(CreateWorkout Workout)
    {
        try
        {
            var existingWorkout = await _context.Workouts.FirstOrDefaultAsync(x => x.Title == Workout.Title);
            if (existingWorkout != null)
                return new Response<string>(HttpStatusCode.BadRequest, "Workout already exists");
            var mapped = _mapper.Map<Workout>(Workout);

            await _context.Workouts.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Workout");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteWorkoutAsync(int id)
    {
        try
        {
            var Workouts = await _context.Workouts.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (Workouts == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Workouts not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetWorkout>> GetWorkoutByIdAsync(int id)
    {
        try
        {
            var Workouts = await _context.Workouts.FirstOrDefaultAsync(x => x.Id == id);
            if (Workouts == null)
                return new Response<GetWorkout>(HttpStatusCode.BadRequest, "Workout not found");
            var mapped = _mapper.Map<GetWorkout>(Workouts);
            return new Response<GetWorkout>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetWorkout>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetWorkout>>> GetWorkoutsAsync(PaginationFilter filter)
    {
        try
        {
            var Workouts = _context.Workouts.AsQueryable();

            var response = await Workouts
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = Workouts.Count();

            var mapped = _mapper.Map<List<GetWorkout>>(response);
            return new PagedResponse<List<GetWorkout>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetWorkout>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetWorkout>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }



    public async Task<Response<string>> UpdateWorkoutAsync(UpdateWorkout Workout)
    {
        try
        {
            var mapped = _mapper.Map<Workout>(Workout);
            _context.Workouts.Update(mapped);
            var update = await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "Workouts not found");
            return new Response<string>("Workouts updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
