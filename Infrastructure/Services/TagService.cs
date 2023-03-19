using Core.Dtos.TagDtos;
using Core.Entities;
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

    public async Task<TagDto?> GetTagById(int tagId)
    {
        return await _tagRepository.GetAll()
            .Select(t => t.ToTagDto())
            .FirstOrDefaultAsync();
    }

    public async Task<Tag?> FindTagByName(string tagName)
    {
        return await _tagRepository.GetAll()
            .AsNoTracking()
            .Where(tag => tag.Name == tagName)
            .FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Asynchronously converts the dto object into an entity and inserts it asynchronously the database. 
    /// </summary>
    /// <param name="tag">
    ///     The DTO object containing the values of the new tag
    /// </param>
    /// <returns>
    ///     The id value of the new inserted Tag in case of success, or -1 if it was unsuccessfully
    /// </returns>
    public async Task<int> CreateTagAsync(TagDto tag)
    {
        var newTag = await _tagRepository.AddAsync(tag.ToTag());
        if (!await _tagRepository.SaveChangesAsync())
        {
            return -1;
        }

        return newTag.Entity.Id;
    }

    /// <summary>
    ///     Checks if the tag repository is not empty (if there is any tag)
    ///     This method is used only for seeding tags
    /// </summary>
    /// <returns>
    ///     A bool value - True if exists at least a tag, False otherwise
    /// </returns>
    public async Task<bool> CheckIfExistTags()
    {
       var firstTag = await _tagRepository.GetAll().FirstOrDefaultAsync();
       return firstTag is not null;
    }

    public async Task<bool> DeleteTagById(int tagId)
    {
        await _tagRepository.RemoveByIdAsync(tagId);
        return await _tagRepository.SaveChangesAsync();
    }
    
}