using Microsoft.EntityFrameworkCore;
using SimpleAuth.Domain.Entities;
using SimpleAuth.Domain.Interfaces;
using SimpleAuth.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Infrastructure.Persistence.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default)
            => await _context.Users.AsNoTracking().ToListAsync(ct);

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
           => await _context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
            => await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email, ct);

        public async Task<User?> GetByNameAsync(string name, CancellationToken ct = default)
            => await _context.Users.FirstOrDefaultAsync(u => u.Username == name, ct);

        public async Task<User?> GetByNameOrEmailAsync(string name, string email, CancellationToken ct = default)
            => await _context.Users.FirstOrDefaultAsync(u => u.Username == name || u.Email == email, ct);

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default)
            => await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, ct);

        public void Add(User user) => _context.Users.Add(user);
        public void Update(User user) => _context.Users.Update(user);
        public void Delete(User user) => _context.Users.Remove(user);
    }
}
