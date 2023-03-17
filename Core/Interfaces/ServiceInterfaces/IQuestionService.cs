using Core.Dtos.QuestionDtos;

namespace Core.Interfaces.ServiceInterfaces;

public interface IQuestionService
{
    public Task<QuestionDto> GetAllQuestions();

    public Task<QuestionExpandedDto> GetQuestionFullInfo();
    
    
}