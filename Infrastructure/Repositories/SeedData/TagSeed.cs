using Core.EntityExtensions.TagExtensions;
using Core.Interfaces.ServiceInterfaces;

namespace Infrastructure.Repositories.SeedData;

public static class TagSeed
{
    public static async Task SeedTagsAsync(ITagService tagService)
    {
        var tagNames = new List<string>() { "html", "css", "javascript" };
        
        if (!await tagService.CheckIfExistTags())
        {
            foreach (var tagDto in tagNames.Select(tagName => tagName.ToTagDto()))
            {
                if(await tagService.CreateTagAsync(tagDto) < 0)
                {
                    break;
                }
            }
        }  
    }
}