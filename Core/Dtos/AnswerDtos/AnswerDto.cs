using Microsoft.Build.Framework;

namespace Core.Dtos.AnswerDtos;

public class AnswerDto
{
    public int Id { get; set; }
    
    public string AuthorUsername { get; set; }
    
    public string Text { get; set; }
    
    public DateTime DatePosted { get; set; }
    
    public DateTime? LastModifiedDate { get; set; }
    
    public string? PictureUrl { get; set; }
    
    public int Score { get; set; }
}