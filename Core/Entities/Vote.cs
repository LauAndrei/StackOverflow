namespace Core.Entities;

public class Vote : BaseEntity
{
    public User? User { get; set; }
    public int UserId { get; set; }
    
    public bool IsUpVote { get; set; }
    
    public Question? Question { get; set; }
    public int? QuestionId { get; set; }
    
    public Answer? Answer { get; set; }
    public int? AnswerId { get; set; }
}