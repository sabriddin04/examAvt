using Domain.DTOs.UserDto;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Services.UserService;

public interface IUserService
{
    Task<PagedResponse<List<GetUser>>> GetUsersAsync(PaginationFilter filter);
    Task<Response<GetUser>> GetUserByIdAsync(int id);
    Task<Response<string>> CreateUserAsync(CreateUser User);
    Task<Response<string>> UpdateUserAsync(UpdateUser User);
    Task<Response<bool>> DeleteUserAsync(int id);
}
