using Core.Dtos.TagDtos;
using Microsoft.Build.Framework;

namespace Core.Dtos.QuestionDtos;

public class PostQuestionDto
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Text { get; set; }
    
    public string? PictureUrl { get; set; }
    
    public List<TagDto> Tags { get; set; }
}