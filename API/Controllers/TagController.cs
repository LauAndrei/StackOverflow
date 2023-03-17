using API.Dtos.TagDtos;
using Core.Constants;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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

    [HttpGet]
    [Route("GetTagById/{tagId:int}")]
    public async Task<TagDto> GetTagById(int tagId)
    {
        return await _tagService.GetTagById(tagId);
    }

    [HttpPost]
    [Route("CreateTag")]
    public async Task<int> CreateTag(TagDto tagDto)
    {
        return await _tagService.CreateTagAsync(tagDto);
    }

    [HttpDelete]
    [Route("DeleteTagById/{tagId:int}")]
    [Authorize(Roles = ROLES_CONSTANTS.ROLES.MODERATOR)]
    public async Task<bool> DeleteTagById(int tagId)
    {
        return await _tagService.DeleteTagById(tagId);
    }
}