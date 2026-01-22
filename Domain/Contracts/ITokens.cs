using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface ITokens
    {
        public Task<string?> GenerateAccessToken(string userId , string token);
        public Task<RefreshToken> GenerateRefreshToken(User user);
    }
}
