using Core.Dtos.AnswerDtos;

namespace Core.Interfaces.ServiceInterfaces;

public interface IAnswerService
{
    public Task<List<AnswerDto>> GetAllAnswers();

    public Task<AnswerDto> PostAnswer(PostAnswerDto newAnswer, int authorId, string authorUsername);

    public Task<bool> UpdateAnswer(PostAnswerDto updatedAnswer);

    public Task<bool> DeleteAnswer(int answerId);

    public Task<bool> CheckIfUserIsAnswersAuthor(int userId, int answerId);
}