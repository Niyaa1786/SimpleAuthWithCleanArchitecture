using SimpleAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default);
        Task<User> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<User> GetByNameAsync(string name, CancellationToken ct = default);
        Task<User> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<User> GetByNameOrEmailAsync(string name, string email, CancellationToken ct = default);
        Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default);

        void  Add(User user);
        void Update(User user);
        void Delete(User user);

    }
}
