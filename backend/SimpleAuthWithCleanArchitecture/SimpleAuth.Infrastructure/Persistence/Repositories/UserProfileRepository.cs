using Microsoft.EntityFrameworkCore;
using SimpleAuth.Domain.Entities;
using SimpleAuth.Domain.Interfaces;
using SimpleAuth.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Infrastructure.Persistence.Repositories
{
    internal class UserProfileRepository : IUserProfileRepository
    {
        private readonly AppDbContext _context;
        public UserProfileRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<UserProfile>> GetAllAsync(CancellationToken ct = default)
            => await _context.UserProfiles.Include(p => p.User).AsNoTracking().ToListAsync(ct);

        public async Task<UserProfile?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _context.UserProfiles.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<UserProfile?> GetByPhoneNumber(string phoneNumber, CancellationToken ct = default)
            => await _context.UserProfiles.Include(p => p.User).FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber, ct);

        public async Task<UserProfile?> GetByUserIdAsync(Guid id, CancellationToken ct = default)
            => await _context.UserProfiles.Include(p => p.User).FirstOrDefaultAsync(p => p.UserId == id, ct);

        public void Add(UserProfile user) => _context.UserProfiles.Add(user);
        public void Update(UserProfile user) => _context.UserProfiles.Update(user);
        public void Delete(UserProfile user) => _context.UserProfiles.Remove(user);

    }
}
