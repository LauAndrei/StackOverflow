using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }
    
    [HttpGet]
    [Route("GetAllTags")]
    public string GetAllTags()
    {
        return "This will be a list of tags";
    }

    [HttpGet]
    [Route("CheckIfExistsATag")]
    public async Task<bool> CheckIfExistTags()
    {
        return await _tagService.CheckIfExistTags();
    }
}