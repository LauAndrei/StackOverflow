using Core.EntityExtensions.TagExtensions;
using Core.Interfaces.ServiceInterfaces;

namespace Infrastructure.Repositories.SeedData;

public class TagSeed
{
    public static async Task SeedTagsAsync(ITagService tagService)
    {
        List<string> tagNames = new List<string>() { "html", "css", "javascript" };
        
        if (!await tagService.CheckIfExistTags())
        {
            foreach (var tagDto in tagNames.Select(tagName => tagName.ToTagDto()))
            {
                if(!await tagService.CreateTagAsync(tagDto))
                {
                    break;
                }
            }
        }  
    }
}