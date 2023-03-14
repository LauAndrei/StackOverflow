namespace Core.Entities;

public class Answer : BaseEntity
{
    public string Text { get; set; }
    
    public User Author { get; set; }
    public int AuthorId { get; set; }
    
    public DateTime DatePosted { get; set; } = DateTime.Now;
    
    public int QuestionId { get; set; }

    public virtual List<Vote> Votes { get; set; } = new List<Vote>();

    public int Score { get; set; } = 0;
}