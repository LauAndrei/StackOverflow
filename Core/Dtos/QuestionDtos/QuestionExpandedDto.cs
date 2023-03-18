using API.Dtos.TagDtos;
using Core.Dtos.AnswerDtos;

namespace Core.Dtos.QuestionDtos;

public class QuestionExpandedDto
{
    public string Title { get; set; } = null!;
    
    public string Text { get; set; } = null!;

    public string AuthorUsername { get; set; } = null!;

    public string? PictureUrl { get; set; }
    
    public DateTime DatePosted { get; set; }
    
    public DateTime? LastModifiedDate { get; set; }
    
    public virtual List<AnswerDto>? Answers { get; set; }
    
    public virtual List<TagReducedDto>? Tags { get; set; }
    
    public int Score { get; set; }
}