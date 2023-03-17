namespace Core.Dtos.QuestionDtos;

public class QuestionDto
{
    public int Id { get; set; }

    public string AuthorUsername { get; set; } = null!;

    public string Text { get; set; } = null!;

    public string? PictureUrl { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public DateTime? LastModifiedDate { get; set; }
}