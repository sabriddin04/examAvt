using Domain.DTOs.MembershipDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.MembershipService;

public interface IMembershipService
{
    Task<PagedResponse<List<GetMembership>>> GetMembershipsAsync(MembershipFilter filter);
    Task<Response<GetMembership>> GetMembershipByIdAsync(int id);
    Task<Response<string>> CreateMembershipAsync(CreateMembership Membership);
    Task<Response<string>> UpdateMembershipAsync(UpdateMembership Membership);
    Task<Response<bool>> DeleteMembershipAsync(int id);
}
