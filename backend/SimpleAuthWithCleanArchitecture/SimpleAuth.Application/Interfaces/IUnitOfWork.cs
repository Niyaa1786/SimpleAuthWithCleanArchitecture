using SimpleAuth.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository Users { get; }

        public Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
