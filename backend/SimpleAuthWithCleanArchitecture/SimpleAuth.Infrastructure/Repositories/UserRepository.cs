using SimpleAuth.Domain.Entities;
using SimpleAuth.Domain.Interfaces;
using SimpleAuth.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) => _context = context;

        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByNameAsync(string name, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByNameOrEmailAsync(string name, string email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
