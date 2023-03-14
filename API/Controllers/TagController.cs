using API.Dtos.TagDtos;
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
    public async Task<List<TagDto>> GetAllTags()
    {
        return await _tagService.GetAllTagsAsync();
    }

    [HttpGet]
    [Route("CheckIfExistsATag")]
    public async Task<bool> CheckIfExistTags()
    {
        return await _tagService.CheckIfExistTags();
    }
}