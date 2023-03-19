using System.Security.Claims;
using Core.Constants;

namespace API.Extensions.ClaimsExtensions;

public static class ClaimPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));
        try
        {
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {   
                throw new Exception(RESPONSE_CONSTANTS.USER.NOT_FOUND);
            }

            return int.Parse(userId);
        }
        catch (Exception ex)
        {
            throw new BadHttpRequestException("Error");
        }
    }
}