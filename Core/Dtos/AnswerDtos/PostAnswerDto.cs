using Microsoft.Build.Framework;

namespace Core.Dtos.AnswerDtos;

/// <summary>
///     Used for Posting a new answer and for Updating an existing answer.
/// </summary>
public class PostAnswerDto
{
    public int Id { get; set; }
    
    [Required]
    public string Text { get; set; }

    [Required]
    public int QuestionId { get; set; }
    
    public string? PictureUrl { get; set; }
}