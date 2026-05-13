using SimpleAuth.Application.DTOs.Request.Auth;
using SimpleAuth.Application.DTOs.Response.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Facades.User
{
    public interface IUserFacade
    {
        public Task<AuthResponse> Register(RegisterRequest request, CancellationToken ct = default);
        public Task<AuthResponse> Login(LoginRequest request, CancellationToken ct = default);
        public Task<bool> Logout(Guid id, CancellationToken ct = default);
        public Task<AuthResponse> RefreshToken(RefreshTokenRequest request, CancellationToken ct = default);
    }
}
