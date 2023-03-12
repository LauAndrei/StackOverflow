using Core.Entities;
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

    public async Task<bool> CreateTagAsync(string tagName)
    {
        throw new NotImplementedException();
    }
}