using API.Dtos.TagDtos;
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

    public async Task<List<Tag>> GetAllTagsAsync()
    {
        throw new NotImplementedException();
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
       if (firstTag is null)
       {
           return false;
       }

       return true;
    }
    
}