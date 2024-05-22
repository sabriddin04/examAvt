using AutoMapper;
using Domain.Responses;
using Domain.DTOs.ClassSchduleDto;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Domain.Entities;
using Domain.Filters;
using System.Data.Common;

namespace Infrastructure.Services.ClassSchduleService;

public class ClassScheduleService : IClassScheduleService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ClassScheduleService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> CreateClassScheduleAsync(CreateClassSchedule ClassSchedule)
    {
        try
        {
            var mapped = _mapper.Map<ClassSchedule>(ClassSchedule);

            await _context.ClassSchedules.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new ClassSchedule");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteClassScheduleAsync(int id)
    {
        try
        {
            var ClassSchdules = await _context.ClassSchedules.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (ClassSchdules == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "ClassSchdules not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetClassSchedule>> GetClassScheduleByIdAsync(int id)
    {
        try
        {
            var ClassSchedules = await _context.ClassSchedules.FirstOrDefaultAsync(x => x.Id == id);
            if (ClassSchedules == null)
                return new Response<GetClassSchedule>(HttpStatusCode.BadRequest, "ClassSchedule not found");
            var mapped = _mapper.Map<GetClassSchedule>(ClassSchedules);
            return new Response<GetClassSchedule>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetClassSchedule>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetClassSchedule>>> GetClassSchedulesAsync(PaginationFilter filter)
    {
        try
        {
            var ClassSchedules = _context.ClassSchedules.AsQueryable();


            var response = await ClassSchedules
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = ClassSchedules.Count();

            var mapped = _mapper.Map<List<GetClassSchedule>>(response);
            return new PagedResponse<List<GetClassSchedule>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetClassSchedule>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetClassSchedule>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }



    public async Task<Response<string>> UpdateClassScheduleAsync(UpdateClassSchedule ClassSchedule)
    {
        try
        {
            var mapped = _mapper.Map<ClassSchedule>(ClassSchedule);
            _context.ClassSchedules.Update(mapped);
            var update = await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "ClassSchedules not found");
            return new Response<string>("ClassSchedules updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
