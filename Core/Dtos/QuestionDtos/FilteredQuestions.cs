namespace Core.Dtos.QuestionDtos;

public class FilteredQuestions
{
    public List<QuestionDto> Questions { get; set; }
    
    public int TotalNumberOfQuestions { get; set; }
}