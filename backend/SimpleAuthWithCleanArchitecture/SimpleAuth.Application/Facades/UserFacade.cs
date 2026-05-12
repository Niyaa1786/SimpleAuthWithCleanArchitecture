using SimpleAuth.Application.DTOs.Request;
using SimpleAuth.Application.DTOs.Response;
using SimpleAuth.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SimpleAuth.Application.Facades
{
    internal class UserFacade : IUserFacade
    {
        private readonly RegisterUseCase _register;
        private readonly LoginUseCase _login;
        private readonly LogoutUseCase _logout;
        private readonly RefreshTokenUseCase _refreshToken;

        public UserFacade(
            RegisterUseCase register,
            LoginUseCase login,
            LogoutUseCase logout,
            RefreshTokenUseCase refreshToken
            )
        {
            _register = register;
            _login = login;
            _logout = logout;
            _refreshToken = refreshToken;
        }
        public Task<AuthResponse> Login(LoginRequest request, CancellationToken ct = default)
            => _login.ExecuteAsync(request, ct);

        public Task<bool> Logout(Guid id, CancellationToken ct = default)
            => _logout.ExecuteAsync(id, ct);

        public Task<AuthResponse> RefreshToken(RefreshTokenRequest request, CancellationToken ct = default)
            => _refreshToken.ExecuteAsync(request, ct);

        public Task<AuthResponse> Register(RegisterRequest request, CancellationToken ct = default)
            => _register.ExecuteAsync(request, ct);
    }
}
