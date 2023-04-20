using Core.Dtos.TagDtos;
using Core.Entities;

namespace Core.Interfaces.ServiceInterfaces;

public interface ITagService
{
    public Task<List<TagDto>> GetAllTagsAsync();

    public Task<TagDto?> GetTagById(int tagId);

    public Task<Tag?> FindTagByName(string tagName);

    public Task<TagDto?> CreateTagAsync(TagDto tagDto);

    public Task<bool> CheckIfExistTags();

    public Task<bool> DeleteTagById(int id);
}