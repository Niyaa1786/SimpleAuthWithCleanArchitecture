using SimpleAuth.Application.DTOs.Shared;
using SimpleAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Interfaces.Common
{
    public interface ITokenGenerator
    {
        public TokenResult GenerateToken(User user);
    }
}
