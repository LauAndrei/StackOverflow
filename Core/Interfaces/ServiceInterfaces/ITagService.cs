using API.Dtos.TagDtos;
using Core.Entities;

namespace Core.Interfaces.ServiceInterfaces;

public interface ITagService
{
    public Task<List<Tag>> GetAllTagsAsync();

    public Task<bool> CreateTagAsync(TagDto tagDto);

    public Task<bool> CheckIfExistTags();
}