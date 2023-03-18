using Core.Dtos.QuestionDtos;
using Core.Entities;

namespace Core.Interfaces.ServiceInterfaces;

public interface IQuestionService
{
    public Task<List<QuestionDto>> GetAllQuestions();

    public Task<bool> CheckUserIsQuestionsAuthor(int questionId, int authorId);

    public Task<QuestionExpandedDto> GetQuestionFullInfo(int id);

    public Task<int> PostQuestion(PostQuestionDto newQuestion, int authorId);

    public Task<bool> DeleteQuestion(int questionId);
}