using API.Dtos.TagDtos;
using Core.Constants;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost]
    [Route("CreateTag")]
    public async Task<int> CreateTag(TagDto tagDto)
    {
        return await _tagService.CreateTagAsync(tagDto);
    }

    [HttpDelete]
    [Route("DeleteTagByName")]
    [Authorize(Roles = ROLES_CONSTANTS.ROLES.MODERATOR)]
    public async Task<bool> DeleteTagByName(TagDto tagDto)
    {
        var foundTag = await _tagService.FindTagByName(tagDto.Name.ToLower());
        if (foundTag is null)
        {
            throw new Exception($"Tag with name {tagDto.Name} does not exist!");
        }
        return await _tagService.DeleteTagById(foundTag.Id);
    }
}