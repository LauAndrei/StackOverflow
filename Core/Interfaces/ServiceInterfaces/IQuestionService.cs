using Core.Dtos.QuestionDtos;

namespace Core.Interfaces.ServiceInterfaces;

public interface IQuestionService
{
    public Task<List<QuestionDto>> GetAllQuestions();

    public Task<bool> CheckUserIsQuestionsAuthor(int authorId, int questionId);

    public Task<QuestionExpandedDto> GetQuestionFullInfo(int id);

    public Task<bool> PostQuestion(PostQuestionDto newQuestion, int authorId);

    public Task<bool> UpdateQuestion(PostQuestionDto question);

    public Task<bool> DeleteQuestion(int questionId);

    public Task<FilteredQuestions> GetPaginatedAndFilteredQuestions(QuestionFilters filters, int pageNumber,
        int pageSize);

    public Task<List<QuestionDto>> GetUsersQuestion(string username);
}