using SimpleAuth.Application.Interfaces;
using SimpleAuth.Domain.Interfaces;
using SimpleAuth.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IUserRepository? _userRepository;
        public UnitOfWork(AppDbContext context) => _context = context;

        public IUserRepository Users => _userRepository ?? new UserRepository(_context);

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
             return _context.SaveChangesAsync(ct);
        }
    }
}
