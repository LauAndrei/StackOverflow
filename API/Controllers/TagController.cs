using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    [HttpGet]
    [Route("GetAllTags")]
    public string GetTags()
    {
        return "This will be a list of tags";
    }

    [HttpGet]
    [Route("TestAuth")]
    [Authorize]
    public string TestAuth()
    {
        return "You should see this only if you are authorized";
    }

}