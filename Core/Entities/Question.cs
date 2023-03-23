namespace Core.Entities;

public class Question : BaseEntity
{
    public string Title { get; set; }
    
    public string Text { get; set; } = null!;
    
    public string Slug { get; set; }
    
    public User? Author { get; set; }
    public int AuthorId { get; set; }
    
    public string? PictureUrl { get; set; }
    
    public DateTime DatePosted { get; set; } = DateTime.Now;
    
    public DateTime? LastModifiedDate { get; set; }

    public virtual List<Answer>? Answers { get; set; } = new List<Answer>();

    public virtual List<QuestionTag>? Tags { get; set; } = new List<QuestionTag>();

    public virtual List<Vote>? Votes { get; set; } = new List<Vote>();

    public int Score { get; set; } = 0;
}