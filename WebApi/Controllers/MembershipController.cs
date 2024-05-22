using Domain.Responses;
using Domain.DTOs.MembershipDto;
using Microsoft.AspNetCore.Mvc;
using Domain.Filters;
using Infrastructure.Services.MembershipService;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Seed;

namespace WEBAPI.Cotrollers;
[ApiController]
[Route("[controller]")]

public class MembershipController(IMembershipService membershipService) : ControllerBase
{
    [HttpGet]
   [Authorize(Roles.Admin)]
       public async Task<Response<List<GetMembership>>> GetMembershipsAsync([FromQuery]MembershipFilter MembershipFilter)
        => await membershipService.GetMembershipsAsync(MembershipFilter);

    [HttpGet("{MembershipId:int}")]
    [Authorize]
    public async Task<Response<GetMembership>> GetMembershipByIdAsync(int MembershipId)
        => await membershipService.GetMembershipByIdAsync(MembershipId);

    [HttpPost("create")]
     [Authorize(Roles.Admin)]
    public async Task<Response<string>> CreateMembershipAsync([FromBody]CreateMembership Membership)
        => await membershipService.CreateMembershipAsync(Membership);


    [HttpPut("update")]
    [Authorize(Roles.Admin)]
    public async Task<Response<string>> UpdateMembershipAsync([FromBody]UpdateMembership Membership)
        => await membershipService.UpdateMembershipAsync(Membership);
    [Authorize(Roles.Admin)]
    [HttpDelete("{MembershipId:int}")]
    public async Task<Response<bool>> DeleteMembershipAsync(int MembershipId)
        => await membershipService.DeleteMembershipAsync(MembershipId);
}
