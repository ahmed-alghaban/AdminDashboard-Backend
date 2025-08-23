using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AdminDashboard.src.Utilities
{
    public static class GetUserIDFromToken
    {
        public static Guid GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Debug: Log all available claims
            var availableClaims = string.Join(", ", jwtToken.Claims.Select(c => $"{c.Type}: {c.Value}"));
            Console.WriteLine($"Available claims in token: {availableClaims}");

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                // Try alternative claim types
                userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")
                             ?? jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")
                             ?? jwtToken.Claims.FirstOrDefault(c => c.Type == "user_id");

                if (userIdClaim == null)
                {
                    throw new UnauthorizedAccessException($"User ID claim not found in token. Available claims: {availableClaims}");
                }
            }

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid user ID format in token");
            }

            return userId;
        }

        public static Guid GetCurrentUserId(IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("No authorization token provided");
            }
            return GetUserIdFromToken(token);
        }
    }
}