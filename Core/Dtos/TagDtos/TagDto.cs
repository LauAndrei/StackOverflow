using Microsoft.Build.Framework;

namespace Core.Dtos.TagDtos;

public class TagDto
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}