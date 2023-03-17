namespace Core.Entities;

public class Answer : BaseEntity
{
    public string Text { get; set; } = null!;
    
    public User? Author { get; set; }
    public int AuthorId { get; set; }
    
    public DateTime DatePosted { get; set; } = DateTime.Now;
    
    public DateTime? LastModifiedDate { get; set; }
    
    public string? PictureUrl { get; set; }
    
    public Question? Question { get; set; }
    public int QuestionId { get; set; }

    public virtual List<Vote>? Votes { get; set; } = new List<Vote>();

    public int Score { get; set; } = 0;
}