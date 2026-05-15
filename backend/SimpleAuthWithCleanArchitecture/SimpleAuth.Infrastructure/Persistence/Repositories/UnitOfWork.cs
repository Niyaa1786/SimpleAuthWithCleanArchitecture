using SimpleAuth.Application.Interfaces.Shared;
using SimpleAuth.Domain.Interfaces;
using SimpleAuth.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Infrastructure.Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IUserRepository? _userRepository;
        private IUserProfileRepository? _userProfileRepository;

        public UnitOfWork(AppDbContext context) => _context = context;

        public IUserRepository Users => _userRepository ??= new UserRepository(_context);
        public IUserProfileRepository UserProfiles => _userProfileRepository ??= new UserProfileRepository(_context);

        public Task<int> SaveChangesAsync(CancellationToken ct = default) => _context.SaveChangesAsync(ct);
    }
}
