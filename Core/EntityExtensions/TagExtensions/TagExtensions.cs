using API.Dtos.TagDtos;
using Core.Entities;

namespace Core.EntityExtensions.TagExtensions;

public static class TagExtensions
{
    public static Tag ToTag(this TagDto tagDto)
    {
        return new Tag
        {
            Id = tagDto.Id,
            Name = tagDto.Name.ToLower()
        };
    }

    public static TagDto ToTagDto(this string tagName)
    {
        return new TagDto
        {
            Name = tagName.ToLower()
        };
    }

    public static TagDto ToTagDto(this Tag tag)
    {
        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }
}