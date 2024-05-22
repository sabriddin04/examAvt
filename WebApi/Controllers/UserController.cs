using Domain.Responses;
using Domain.DTOs.UserDto;
using Domain.Filters;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WEBAPI.Cotrollers;
[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<Response<List<GetUser>>> GetUsersAsync([FromQuery]PaginationFilter userFilter)
        => await userService.GetUsersAsync(userFilter);

    [HttpGet("{userId:int}")]
    public async Task<Response<GetUser>> GetUserByIdAsync(int userId)
        => await userService.GetUserByIdAsync(userId);

    [HttpPost("create")]

    public async Task<Response<string>> CreateUserAsync([FromBody]CreateUser user)
        => await userService.CreateUserAsync(user);


    [HttpPut("update")]
    public async Task<Response<string>> UpdateUserAsync([FromBody]UpdateUser user)
        => await userService.UpdateUserAsync(user);

    [HttpDelete("{userId:int}")]
    public async Task<Response<bool>> DeleteUserAsync(int userId)
        => await userService.DeleteUserAsync(userId);
}
