using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Interfaces.Common
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
        public bool Verify(string password, string hashPassword);
    }
}
