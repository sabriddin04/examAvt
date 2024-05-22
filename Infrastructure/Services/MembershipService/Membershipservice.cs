using AutoMapper;
using Domain.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Domain.DTOs.MembershipDto;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Domain.Filters;
using System.Data.Common;

namespace Infrastructure.Services.MembershipService;

public class MembershipService : IMembershipService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public MembershipService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<Response<string>> CreateMembershipAsync(CreateMembership Membership)
    {
        try
        {
            var mapped = _mapper.Map<Membership>(Membership);

            await _context.Memberships.AddAsync(mapped);
            await _context.SaveChangesAsync();

            return new Response<string>("Successfully created a new Membership");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteMembershipAsync(int id)
    {
        try
        {
            var Memberships = await _context.Memberships.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (Memberships == 0)
                return new Response<bool>(HttpStatusCode.BadRequest, "Memberships not found");

            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetMembership>> GetMembershipByIdAsync(int id)
    {
        try
        {
            var Memberships = await _context.Memberships.FirstOrDefaultAsync(x => x.Id == id);
            if (Memberships == null)
                return new Response<GetMembership>(HttpStatusCode.BadRequest, "Membership not found");
            var mapped = _mapper.Map<GetMembership>(Memberships);
            return new Response<GetMembership>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetMembership>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<PagedResponse<List<GetMembership>>> GetMembershipsAsync(MembershipFilter filter)
    {
        try
        {
            var Memberships = _context.Memberships.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Type))
                Memberships = Memberships.Where(x => x.Type.ToLower().Contains(filter.Type.ToLower()));
            var response = await Memberships
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            var totalRecord = Memberships.Count();

            var mapped = _mapper.Map<List<GetMembership>>(response);
            return new PagedResponse<List<GetMembership>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);

        }
        catch (DbException dbEx)
        {
            return new PagedResponse<List<GetMembership>>(HttpStatusCode.InternalServerError, dbEx.Message);
        }
        catch (Exception ex)
        {
            return new PagedResponse<List<GetMembership>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }



    public async Task<Response<string>> UpdateMembershipAsync(UpdateMembership Membership)
    {
        try
        {
            var mapped = _mapper.Map<Membership>(Membership);
            _context.Memberships.Update(mapped);
            var update = await _context.SaveChangesAsync();
            if(update==0)  return new Response<string>(HttpStatusCode.BadRequest, "Memberships not found");
            return new Response<string>("Memberships updated successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}
