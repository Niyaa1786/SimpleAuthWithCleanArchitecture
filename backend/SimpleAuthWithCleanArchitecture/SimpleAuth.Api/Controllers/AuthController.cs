using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleAuth.Api.Responses;
using SimpleAuth.Application.DTOs.Request.Auth;
using SimpleAuth.Application.DTOs.Response.Auth;
using SimpleAuth.Application.Facades.User;

namespace SimpleAuth.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserFacade _userFacade;
        public AuthController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request, CancellationToken ct)
        {
            var result = await _userFacade.Register(request,ct);
            var res = ApiResponse<AuthResponse>.Sucesss(result, "Register successfully");

            return Ok(res);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct)
        {
            var result = await _userFacade.Login(request, ct);
            var res = ApiResponse<AuthResponse>.Sucesss(result, "Login sucessfully");

            return Ok(res);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(Guid id, CancellationToken ct)
        {
            await _userFacade.Logout(id, ct);
            var res = ApiResponse<object>.Sucesss(null!, "Logout sucessfully");

            return Ok(res);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request, CancellationToken ct)
        {
            var result = await _userFacade.RefreshToken(request, ct);
            var res = ApiResponse<AuthResponse>.Sucesss(result, "Token refreshed");

            return Ok(res);
        }
    }
}
