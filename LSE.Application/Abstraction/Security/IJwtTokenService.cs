using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Abstraction.Security
{
    public interface IJwtTokenService
    {
        /// <summary>
        /// Generates a JWT token using claims for the authenticated broker user.
        /// </summary>
        /// <param name="claims">Dictionary of claims representing the user identity</param>
        /// <param name="expiresInHours">How many hours the token is valid</param>
        /// <returns>JWT token string + expiry timestamp UTC</returns>
        (string Token, DateTime ExpiresAtUtc) GenerateToken(
            IDictionary<string, string> claims,
            int expiresInHours = 6);
    }
}
