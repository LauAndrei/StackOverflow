using Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestAuthController : ControllerBase
{
   [HttpGet]
   [Route("TestHit")]
   public string TestHit()
   {
      return "Good endpoint";
   }
   
   [HttpGet]
   [Route("TestAuthBasic")]
   [Authorize]
   public string TestAuthBasic()
   {
      return "You should see this only if you are authenticated";
   }

   [HttpGet]
   [Route("TestAuthStandard")]
   [Authorize(Roles = ROLES_CONSTANTS.ROLES.STANDARD)]
   public string TestAuthStandard()
   {
      return "You should see this only if you are authenticated as standard user";
   }

   [HttpGet]
   [Route("TestAuthModerator")]
   [Authorize(Roles = ROLES_CONSTANTS.ROLES.MODERATOR)]
   public string TestAuthModerator()
   {
      return "You should see this only if you are authenticated as moderator";
   }
}