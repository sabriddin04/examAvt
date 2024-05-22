using Domain.DTOs.AuthDto;
using Domain.Responses;

namespace Infrastructure.Services.AuthService;

public interface IAuthService
{
    Task<Response<string>> Login(LoginDto loginDto);
    Task<Response<string>> Register(RegisterDto registerDto);
}