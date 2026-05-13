using SimpleAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Domain.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken ct = default);
        Task<UserProfile?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<UserProfile?> GetByUserIdAsync(Guid id, CancellationToken ct = default);
        Task<UserProfile?> GetByPhoneNumber(string phoneNumber, CancellationToken ct = default);

        void Add(UserProfile user);
        void Update(UserProfile user);
        void Delete(UserProfile user);
    }
}
