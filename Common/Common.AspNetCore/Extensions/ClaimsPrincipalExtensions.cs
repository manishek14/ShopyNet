using System.Security.Claims;

namespace Common.AspNetCore.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            if (user == null || !user.Identity.IsAuthenticated)
                throw new UnauthorizedAccessException("User is not authenticated.");

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? user.FindFirst("UserId")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                throw new UnauthorizedAccessException("UserId claim not found.");

            return Guid.Parse(userIdClaim);
        }

        public static bool IsLoggedIn(this ClaimsPrincipal user)
        {
            return user?.Identity?.IsAuthenticated == true;
        }
    }
}