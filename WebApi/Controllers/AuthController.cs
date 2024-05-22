using Domain.DTOs.AuthDto;
using Domain.Responses;
using Infrastructure.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService service) : ControllerBase
{
    [HttpPost("/login")]
    public async Task<Response<string>> Login(LoginDto loginDto)
        => await service.Login(loginDto);

    [HttpPost("/register")]
    public async Task<Response<string>> Register(RegisterDto registerDto)
        => await service.Register(registerDto);
}