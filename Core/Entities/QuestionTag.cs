namespace Core.Entities;

public class QuestionTag : BaseEntity
{
    public Question Question { get; set; }
    public int QuestionId { get; set; }
    
    public Tag Tag { get; set; }
    public int TagId { get; set; }
}