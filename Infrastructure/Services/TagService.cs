using API.Dtos.TagDtos;
using Core.EntityExtensions.TagExtensions;
using Core.Interfaces.ServiceInterfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TagService : ITagService
{
    private readonly TagRepository _tagRepository;

    public TagService(TagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<List<TagDto>> GetAllTagsAsync()
    {
        return await _tagRepository.GetAll()
            .Select(t => t.ToTagDto())
            .ToListAsync();
    }

    public async Task<bool> CreateTagAsync(TagDto tag)
    {
        var newTag = tag.ToTag();
        await _tagRepository.AddAsync(newTag);
        return await _tagRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Checks if the tag repository is not empty (if there is any tag)
    /// This method is used only for seeding tags
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CheckIfExistTags()
    {
       var firstTag = await _tagRepository.GetAll().FirstOrDefaultAsync();
       return firstTag is not null;
    }
    
}