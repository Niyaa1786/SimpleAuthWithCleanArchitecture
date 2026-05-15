using SimpleAuth.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Interfaces.Shared
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }
        public IUserProfileRepository UserProfiles { get; }

        public Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
