namespace Core.Entities;

public class Question : BaseEntity
{
    public string Text { get; set; }
    
    public User Author { get; set; }
    public int AuthorId { get; set; }
    
    public DateTime DatePosted { get; set; } = DateTime.Now;

    public virtual List<Answer> Answers { get; set; } = new List<Answer>();

    public virtual List<QuestionTag> Tags { get; set; } = new List<QuestionTag>();

    public virtual List<Vote> Votes { get; set; } = new List<Vote>();

    public int Score { get; set; } = 0;
}