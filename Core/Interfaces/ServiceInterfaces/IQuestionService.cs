using Core.Dtos.QuestionDtos;
using Core.Entities;

namespace Core.Interfaces.ServiceInterfaces;

public interface IQuestionService
{
    public Task<List<QuestionDto>> GetAllQuestions();

    public Task<bool> CheckUserIsQuestionsAuthor(int authorId, int questionId);

    public Task<QuestionExpandedDto> GetQuestionFullInfo(int id);

    public Task<int> PostQuestion(PostQuestionDto newQuestion, int authorId);

    public Task<bool> UpdateQuestion(PostQuestionDto question);

    public Task<bool> DeleteQuestion(int questionId);
}